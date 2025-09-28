using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsManager : MonoBehaviour
{

    [SerializeField] private Button characterStatButton;

    [SerializeField] private GameObject characterStatsHUD;


    [SerializeField] private GameObject characterSelectionPrefab;
    [SerializeField] private Transform characterSelectionContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterStatButton.onClick.AddListener(() => ShowTab("CharacterSelection"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowTab(string type)
    {

    }
}
