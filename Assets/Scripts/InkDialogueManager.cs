using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InkDialogueManager : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private Button choiceButtonPrefab;

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private RectTransform clickZone;
    [SerializeField] private Camera eventCamera;

    // === Fade sequence support ===
    [Header("Fade Overlay")]
    [SerializeField] private Image fadeImage;   // full-screen black CG with alpha 0
    [SerializeField] private float fadeDuration;
    [SerializeField] private TextMeshProUGUI fadeText;


    [Header("Audio (Typewriter)")]
    [SerializeField] private AudioSource sfxSource;     // assign in Inspector
    [SerializeField] private AudioClip typeBlip;        // a short blip/tick
    [SerializeField] private bool randomizeStartTime = true;
    private bool typingSfxPlaying = false;

    // Add these to your Audio (Typewriter) section / Params section
    [SerializeField] private float spaceDelayFactor = 0.2f;     // spaces are quicker
    [SerializeField] private float commaPause = 0.15f;
    [SerializeField] private float periodPause = 0.35f;
    [SerializeField] private float questionPause = 0.35f;
    [SerializeField] private float exclaimPause = 0.30f;
    [SerializeField] private float ellipsisPause = 0.50f;
    [SerializeField] private float newlinePause = 0.25f;

    // Optional: separate pitch ranges for letters vs punctuation
    [SerializeField] private Vector2 letterPitchJitter = new Vector2(0.98f, 1.04f);
    [SerializeField] private Vector2 punctuationPitchJitter = new Vector2(0.92f, 0.98f);

  

    private Story story;

    
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool isInCombat = false;
    private bool choicesShownThisLine = false;


    private string heldStory = "";


    public static InkDialogueManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        story = new Story(inkJSON.text);
        story.BindExternalFunction("StartCombat", (string enemy, string continueKnot) => StartCombat(enemy, continueKnot));
        story.BindExternalFunction("AddWeapon", (string weapon) => InventoryManager.Instance.AddWeapon(weapon));
        story.BindExternalFunction("AddItem", (string item) => InventoryManager.Instance.AddItem(item));
        story.BindExternalFunction("FadeOutSeq", (string pipeSeparated, string continueKnot) => FadeOutSeq(pipeSeparated, continueKnot));
        story.BindExternalFunction("AddCharacter", (string character) => GameController.Instance.AddCharacter(character));
        story.BindExternalFunction("RemoveCharacter", (string character) => GameController.Instance.RemoveCharacter(character));
        story.BindExternalFunction("PlaySong", (string song) =>  SongPlayer.Instance.PlaySong(song));
        story.BindExternalFunction("StopSong", () => SongPlayer.Instance.StopSong());

        story.BindExternalFunction("OpenShop", (string shopName, string continueKnot) => ShopManager.Instance.OpenShop(shopName, continueKnot));
        story.BindExternalFunction("UnlockLocation", (string location) => MapManager.Instance.UnlockLocation(location));
        story.BindExternalFunction("UnlockSubLocation", (string sublocation) => MapManager.Instance.UnlockSubLocation(sublocation));
        story.BindExternalFunction("RevealMapButton", () => MapManager.Instance.RevealMapButton());
        story.BindExternalFunction("RevealInventoryButton", () => InventoryManager.Instance.RevealInventoryButton());
        ContinueStory();
    }

    void Update() 
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                clickZone, Input.mousePosition, eventCamera, out localMousePos);

            if (clickZone.rect.Contains(localMousePos))
            {
                if (story.currentChoices.Count == 0 && story.canContinue && !isTyping && !isInCombat)
                {
                    ContinueStory();
                }
                else if (isTyping)
                {
                    dialogueText.text = heldStory;
                    StopCoroutine(typingCoroutine);
                    isTyping = false;
                    StopTypingSfx();

                    if (!choicesShownThisLine && story.currentChoices.Count > 0)
                    {
                        choicesShownThisLine = true;
                        DisplayChoices();
                    }
                }
            }
        } 
        
    }

    void ContinueStory()
    {
        // Clear existing choices
        foreach (Transform child in buttonContainer.transform)
            Destroy(child.gameObject);

        choicesShownThisLine = false;

        // Show choices
        if (story.canContinue)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            heldStory = story.Continue();

            typingCoroutine = StartCoroutine(DisplayLine(heldStory, dialogueText));
            
        } 
        else
        {
            StopTypingSfx();
            // No choices = end of story
            Button endButton = Instantiate(choiceButtonPrefab, buttonContainer.transform);
            endButton.GetComponentInChildren<TextMeshProUGUI>().text = "End";
            endButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

    }

    private void DisplayChoices()
    {
        foreach (Choice choice in story.currentChoices)
        {
            Button button = Instantiate(choiceButtonPrefab, buttonContainer.transform);
            string formattedChoice = DialogueFormatter.ConvertItalics(choice.text.Trim());
            button.GetComponentInChildren<TextMeshProUGUI>().text = formattedChoice;

            button.onClick.AddListener(() =>
            {
                story.ChooseChoiceIndex(choice.index);
                story.Continue();
                ContinueStory();
            });
        }
    }

    IEnumerator DisplayLine(string line, TextMeshProUGUI textBox) 
    {
        isTyping = true;
        textBox.text = "";

        bool insideTag = false;
        bool audioSuppressedThisLine = false;
        StartTypingSfx();

        int i = 0;
        while (i < line.Length)
        {
            char c = line[i];

            if (c == '<') insideTag = true;

            float wait = typingSpeed;
            string toAppend = c.ToString();

            bool isPunctPause = false;   // <-- will we pause the SFX this step?

            if (!insideTag)
            {
                // Whitespace quick
                if (char.IsWhiteSpace(c))
                {
                    if (c == '\n' || c == '\r')
                    {
                        wait = newlinePause;
                        StopTypingSfx();
                        audioSuppressedThisLine = true;
                    }
                    else
                    {
                        wait = typingSpeed * spaceDelayFactor;
                        if (sfxSource) sfxSource.pitch = Random.Range(letterPitchJitter.x, letterPitchJitter.y);
                    }
                }
                else
                {
                    // Ellipsis as single unit
                    if (c == '.' && i + 2 < line.Length && line[i + 1] == '.' && line[i + 2] == '.')
                    {
                        toAppend = "...";
                        i += 2; // consume extra dots
                        wait = ellipsisPause;
                        isPunctPause = true;
                    }
                    else if (c == '.')
                    {
                        wait = periodPause;
                        isPunctPause = true;
                    }
                    else if (c == ',')
                    {
                        wait = commaPause;
                        isPunctPause = true;
                    }
                    else if (c == '?') 
                    { 
                        wait = questionPause; 
                        isPunctPause = true; 
                    }
                    else if (c == '!') 
                    { 
                        wait = exclaimPause; 
                        isPunctPause = true; 
                    }
                    else
                    {
                        // normal visible character
                        wait = typingSpeed;
                    }

                    // Pitch gesture
                    if (sfxSource)
                    {
                        if (isPunctPause)
                            sfxSource.pitch = Random.Range(punctuationPitchJitter.x, punctuationPitchJitter.y);
                        else
                            sfxSource.pitch = Random.Range(letterPitchJitter.x, letterPitchJitter.y);
                    }
                }
            }

            if (c == '>') insideTag = false;

            // Append the visible chunk (handles ellipsis as one step)
            textBox.text += toAppend;

            // If user skipped mid-typing, stop immediately
            if (!isTyping) break;

            // For comma/period/ellipsis: pause the looping SFX during the stall
            if (isPunctPause) PauseTypingSfx();

            yield return new WaitForSeconds(wait);

            if (!isTyping) break;

            if (isPunctPause && !audioSuppressedThisLine)
                ResumeTypingSfx();

            i++;
        }

        isTyping = false;
        StopTypingSfx();

        if (!choicesShownThisLine && story.currentChoices.Count > 0)
        {
            choicesShownThisLine = true;
            DisplayChoices();
        }
    }


    private void StartCombat(string encounterID, string continueKnot)
    {
       
        Debug.Log($"Starting combat with {encounterID}");
        gameController.StartCombat(encounterID, continueKnot);
    }

    public void SetCombatState(bool state, string continueKnot)
    {
        isInCombat = state;
        if (!state && !string.IsNullOrEmpty(continueKnot))
        {
            story.ChoosePathString(continueKnot);
            ContinueStory();
        }
    }

    public void FadeOutSeq(string pipeSeperated, string continueKnot)
    {
        StartCoroutine(FadeOutSeqCo(pipeSeperated, continueKnot));
    }

    private IEnumerator FadeOutSeqCo(string pipeSeperated, string continueKnot)
    {

        float heldTypingSpeed = typingSpeed;
        typingSpeed = 0.10f;

        yield return StartCoroutine(FadeImageAlpha(fadeImage, 0, 1));
        fadeText.gameObject.SetActive(true);
        var parts = pipeSeperated.Split('/');

        foreach (var raw in parts)
        {
            var line = (raw ?? "").Trim();
            if (string.IsNullOrEmpty(line)) continue;
            yield return StartCoroutine(DisplayLine(line, fadeText));

            yield return new WaitForSeconds(3.0f);
        }

        story.ChoosePathString(continueKnot);
        ContinueStory();
        fadeText.gameObject.SetActive(false);

        typingSpeed = heldTypingSpeed;
        yield return StartCoroutine(FadeImageAlpha(fadeImage, 1, 0));
    }

    private IEnumerator FadeImageAlpha(Image img, float from, float to)
    {
        if (!img) yield break;
        Color c = img.color;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / fadeDuration);
            img.color = c;
            yield return null;
        }
        c.a = to;
        img.color = c;
    }
    private void StartTypingSfx()
    {
        if (sfxSource == null || typeBlip == null || typingSfxPlaying) return;
        sfxSource.clip = typeBlip;
        if (randomizeStartTime && typeBlip.length > 0f)
            sfxSource.time = Random.Range(0f, typeBlip.length);
        sfxSource.loop = true;
        sfxSource.Play();
        typingSfxPlaying = true;
    }

    private void StopTypingSfx()
    {
        if (sfxSource == null || !typingSfxPlaying) return;
        sfxSource.Stop();
        typingSfxPlaying = false;
    }

    private void PauseTypingSfx()
    {
        if (sfxSource != null && typingSfxPlaying && sfxSource.isPlaying)
            sfxSource.Pause();
    }

    private void ResumeTypingSfx()
    {
        // If we were playing and got paused, continue from where we left off
        if (sfxSource != null && typingSfxPlaying && !sfxSource.isPlaying)
            sfxSource.UnPause();
    }


    void OnDisable()
    {
        StopTypingSfx(); // safety in case the object is hidden mid-typing
    }

    public void locationChange(string location)
    {
        story.ChoosePathString(location);
        ContinueStory();
    }
}
