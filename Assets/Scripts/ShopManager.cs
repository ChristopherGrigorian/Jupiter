using System.Collections.Generic;
using Ink.Parsed;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopData
{
    public string shopName;

    [TextArea(3, 10)]
    public string shopDescription;

    public List<ShopItem> shopItems;
    public List<ShopWeapon> shopWeapons;
}

[System.Serializable]
public class ShopItem
{
    public int price;
    public ItemData item; 
}

[System.Serializable]
public class ShopWeapon
{
    public int price;
    public WeaponData weapon;
}

public class ShopManager : MonoBehaviour
{

    public List<ShopData> shopData = new();

    [SerializeField] private Transform featuresContainer;
    [SerializeField] Button weaponButton;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] Button itemButton;
    [SerializeField] private Transform itemContainer;

    [SerializeField] private GameObject buttonPrefab;

    private string previousType = "";

    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject shopHUD;
    [SerializeField] private TextMeshProUGUI shopDescription;
    [SerializeField] private TextMeshProUGUI totalCoinDisplay;

    public static ShopManager Instance;

    private string storyDropoff;
    private ShopData usedShop;

    // Call this from INK
    public void OpenShop(string shopName, string storyRelocate)
    {
        storyDropoff = storyRelocate;
        foreach (var shop in shopData)
        {
            if (shop.shopName == shopName)
            {
                usedShop = shop;
            } 
        }
        ToggleHUD(shopHUD, true);
        shopDescription.text = usedShop.shopDescription;
        totalCoinDisplay.text = $"Total Coin: {InventoryManager.Instance.totalCoin}";
        ShowTab("Features");
    }
    
    private void ToggleHUD(GameObject hud, bool show)
    {
        if (!hud) return;
        var fader = hud.GetComponent<UIFader>();
        if (fader == null)
        {
            Debug.Log("the fader was null");
            // fallback, but strongly recommend adding UIFader to all HUDs
            hud.SetActive(show);
            return;
        }

        if (show) fader.Show();
        else if (hud.activeInHierarchy) fader.Hide();
    }

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponButton.onClick.AddListener(() => ShowTab("Weapons"));
        itemButton.onClick.AddListener(() => ShowTab("Items"));
    }

    public void ShowTab(string type)
    {
        featuresContainer.gameObject.SetActive(type == "Features");
        weaponContainer.gameObject.SetActive(type == "Weapons");
        itemContainer.gameObject.SetActive(type == "Items");

        ClearAll();

        if (type == "Features")
        {
            previousType = "";
        }

        if (type == "Weapons")
        {
            previousType = "Features";
            shopDescription.gameObject.SetActive(false);
            
            foreach (var w in usedShop.shopWeapons)
            {
                var btn = Instantiate(buttonPrefab, weaponContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{w.price} gold: " + w.weapon.weaponName;

                var trigger = btn.AddComponent<WeaponTooltipTrigger>();
                trigger.Init(w.weapon);

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (InventoryManager.Instance.totalCoin >= w.price)
                    {
                        InventoryManager.Instance.AddWeapon(w.weapon.weaponName);
                        InventoryManager.Instance.totalCoin -= w.price;
                        totalCoinDisplay.text = $"Total Coin: {InventoryManager.Instance.totalCoin}";
                        usedShop.shopWeapons.Remove(w);
                        TooltipManager.Instance.HideTooltip();
                        ShowTab("Weapons");
                    }
                });
            }
        }

        if (type == "Items")
        {
            previousType = "Features";
            shopDescription.gameObject.SetActive(false);
            foreach (var i in usedShop.shopItems)
            {
                var btn = Instantiate(buttonPrefab, itemContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{i.price} gold: " + i.item.itemName;

                // Items just use skills internally so we use skilltooltip here.
                var trigger = btn.AddComponent<SkillTooltipTrigger>();
                trigger.Init(i.item.skillAttached);

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (InventoryManager.Instance.totalCoin >= i.price)
                    {
                        InventoryManager.Instance.AddItem(i.item.itemName);
                        InventoryManager.Instance.totalCoin -= i.price;
                        totalCoinDisplay.text = $"Total Coin:  {InventoryManager.Instance.totalCoin}";
                        usedShop.shopItems.Remove(i);
                        TooltipManager.Instance.HideTooltip();
                        ShowTab("Items");
                    }
                });
            }
        }
    }

    public void ClearAll()
    {
        foreach (Transform child in weaponContainer) Destroy(child.gameObject);
        foreach (Transform child in itemContainer) Destroy(child.gameObject);
    }

    public void previousMenu()
    {
        if (previousType == "") 
        { 
            ToggleHUD(shopHUD, false);
            InkDialogueManager.Instance.locationChange(storyDropoff);
        } else
        {
            ShowTab(previousType);
            shopDescription.gameObject.SetActive(true);
        }
    }
}
