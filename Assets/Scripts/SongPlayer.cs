using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    [Header("Audio Songs")]
    [SerializeField] private AudioSource songSource;
    [SerializeField] private List<SongEntry> songClips = new();

    [Header("Defaults")]
    [SerializeField] private float defaultFadeSeconds = 1.5f;

    public static SongPlayer Instance;

    [System.Serializable]
    public class SongEntry
    {
        public string name;              // key used by PlaySong("name")
        public AudioClip clip;
        public bool shouldLoop = false;  // loop behavior
        [Range(0.5f, 2f)] public float basePitch = 1f;
        [Range(0f, 0.25f)] public float pitchJitter = 0.03f; // ± amount applied each loop
        public float fadeSeconds = 3f; // used on non-loop songs when stopping
    }

    private Dictionary<string, SongEntry> byName;
    private Coroutine activeRoutine;
    private Coroutine loopRoutine;
    private string currentName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        } else
        {
            Destroy(this);
        }
        

        byName = new Dictionary<string, SongEntry>();
        foreach (var s in songClips)
        {
            if (s?.clip == null || string.IsNullOrEmpty(s.name)) continue;
            byName[s.name] = s;
        }

    }
    

    public void PlaySong(string songName)
    {
        if (!byName.TryGetValue(songName, out var entry))
        {
            Debug.LogWarning($"PlaySong: no song named '{songName}'");
            return;
        }

        if (activeRoutine != null) { StopCoroutine(activeRoutine); activeRoutine = null; }
        if (loopRoutine != null) { StopCoroutine(loopRoutine); loopRoutine = null; }

        currentName = entry.name;

        songSource.Stop();
        songSource.clip = entry.clip;
        songSource.loop = entry.shouldLoop;
        songSource.pitch = Mathf.Clamp(entry.basePitch, 0.5f, 2f);
        songSource.volume = 1f;
        songSource.Play();

        if (entry.shouldLoop)
        {
            // keep nudging pitch per loop
            loopRoutine = StartCoroutine(PitchPerLoop(entry));
        }
        else
        {
            // play once, then fade out and stop
            float fade = entry.fadeSeconds > 0f ? entry.fadeSeconds : defaultFadeSeconds;
            activeRoutine = StartCoroutine(PlayOnceThenFadeOut(entry.clip, fade));
        }
    }

    public void StopSong(float fadeSeconds = -1f)
    {
        if (!songSource.isPlaying) return;
        if (activeRoutine != null) { StopCoroutine(activeRoutine); activeRoutine = null; }
        if (loopRoutine != null) { StopCoroutine(loopRoutine); loopRoutine = null; }
        activeRoutine = StartCoroutine(FadeOutAndStop(fadeSeconds < 0f ? defaultFadeSeconds : fadeSeconds));
        currentName = null;
    }

    private IEnumerator PlayOnceThenFadeOut(AudioClip clip, float fadeSeconds)
    {
        if (clip == null) yield break;

        // wait for the clip duration, adjusted by pitch
        while (songSource.isPlaying && songSource.clip == clip)
        {
            float seconds = clip.length / Mathf.Max(0.0001f, songSource.pitch);
            // in case pitch changes mid-play (it shouldn't for non-loop), just wait once
            yield return new WaitForSeconds(seconds - fadeSeconds);
            break;
        }

        yield return FadeOutAndStop(fadeSeconds);
        activeRoutine = null;
        currentName = null;
    }

    private IEnumerator PitchPerLoop(SongEntry entry)
    {
        var clip = entry.clip;
        // Defensive: tiny minimum wait to avoid tight loop if data is weird
        const float MIN_WAIT = 0.02f;

        while (songSource.isPlaying && songSource.clip == clip && entry.shouldLoop)
        {
            // Wait for exactly one loop at current pitch
            float loopDur = Mathf.Max(MIN_WAIT, clip.length / Mathf.Max(0.0001f, songSource.pitch));
            yield return new WaitForSeconds(loopDur);

            if (!(songSource.isPlaying && songSource.clip == clip && entry.shouldLoop)) break;

            // nudge pitch around basePitch ± jitter
            float jitter = Random.Range(-entry.pitchJitter, entry.pitchJitter);
            float target = entry.basePitch + jitter;
            songSource.pitch = Mathf.Clamp(target, 0.5f, 2f);
        }
        loopRoutine = null;
    }

    private IEnumerator FadeOutAndStop(float seconds)
    {
        if (seconds <= 0f)
        {
            songSource.Stop();
            yield break;
        }

        float startVol = songSource.volume;
        float t = 0f;
        while (t < seconds && songSource.isPlaying)
        {
            t += Time.deltaTime; // unaffected by timescale
            songSource.volume = Mathf.Lerp(startVol, 0f, t / seconds);
            yield return null;
        }

        songSource.Stop();
        songSource.volume = 1f; // reset for next play
    }
}
