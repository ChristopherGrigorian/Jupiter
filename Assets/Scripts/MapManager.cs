using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class LocationData
{
    public string LocationName;
    public bool isUnlocked;
}

[System.Serializable]
public class SubLocationData
{
    public string subLocationName;
    public bool isUnlocked;
    public string mainLocation;
    public Sprite sprite;
}


public class MapManager : MonoBehaviour
{
    public List<LocationData> unlockableLocations = new();
    public List<SubLocationData> unlockableSubLocations = new();

    [SerializeField] private Transform locationContainer;
    [SerializeField] private Transform sublocationConainter;

    [SerializeField] private Button mapButton;

    [SerializeField] private GameObject buttonPrefab;

    private string previousType = "";
    private string chosenLocation = "";

    [SerializeField] private Image locationImage;
    [SerializeField] private GameObject mapHUD;

    public bool revealedMap = false;

    public static MapManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Start()
    {
        mapButton.onClick.AddListener(() => ShowTab("Locations"));
    }

    public void ShowTab(string type)
    {
        locationContainer.gameObject.SetActive(type == "Locations");
        sublocationConainter.gameObject.SetActive(type == "SubLocations");

        ClearAll();

        if (type == "Locations")
        {
            previousType = "";
            foreach (var location in unlockableLocations)
            {
                if (location.isUnlocked)
                {
                    var btn = Instantiate(buttonPrefab, locationContainer);
                    btn.GetComponentInChildren<TextMeshProUGUI>().text = location.LocationName;
                    btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        chosenLocation = location.LocationName;
                        ShowTab("SubLocations");
                    });
                }
            }
        }

        if (type == "SubLocations")
        {
            previousType = "Locations";
            foreach (var sublocation in unlockableSubLocations)
            {
                if (sublocation.isUnlocked && sublocation.mainLocation == chosenLocation)
                {
                    var btn = Instantiate(buttonPrefab, sublocationConainter);
                    var mapButtonScript = btn.AddComponent<ButtonHoverImage>();
                    mapButtonScript.targetImage = locationImage;
                    mapButtonScript.hoverSprite = sublocation.sprite;

                    btn.GetComponentInChildren<TextMeshProUGUI>().text = sublocation.subLocationName;
                    btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        InkDialogueManager.Instance.locationChange(sublocation.subLocationName);
                        mapHUD.SetActive(false);
                        GameController.Instance.cameraPan.PanTo(GameController.Instance.dialogueCamAnchor);
                    });
                }
            }
        }
    }

    private void ClearAll()
    {
        foreach (Transform child in locationContainer) Destroy(child.gameObject);
        foreach (Transform child in sublocationConainter) Destroy(child.gameObject);
    }

    public void previousMenu()
    {
        if (previousType == "")
        {
            mapHUD.SetActive(false);
            GameController.Instance.cameraPan.PanTo(GameController.Instance.dialogueCamAnchor);
        }
        else
        {
            ShowTab(previousType);
        }
    }

    public void UnlockLocation(string location)
    {
        foreach (var locations in unlockableLocations)
        {
            if (locations.LocationName == location)
            {
                locations.isUnlocked = true;
            }
        }
    }

    public void UnlockSubLocation(string sublocation)
    {
        foreach (var sublocations in unlockableSubLocations)
        {
            if (sublocations.subLocationName == sublocation)
            {
                sublocations.isUnlocked = true;
            }
        }
    }

    public bool IsLocationUnlocked(string location)
    {
        foreach (var locations in unlockableLocations)
        {
            if (locations.LocationName == location)
            {
                return locations.isUnlocked;
            }
        }
        return false;
    }

    public bool IsSubLocationUnlocked(string sublocation)
    {
        foreach (var sublocations in unlockableSubLocations)
        {
            if (sublocations.subLocationName == sublocation)
            {
                sublocations.isUnlocked = true;
            }
        }
        return false;
    }

    public void RevealMapButton()
    {
        if (mapButton != null)
        {
            mapButton.gameObject.SetActive(true);
            revealedMap = true;
        }
    }

}
