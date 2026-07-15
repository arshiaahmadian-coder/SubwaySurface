using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentGoldAmount;

    [SerializeField] AudioClip clickSound;
    [SerializeField] List<GameObject> skinModels = new List<GameObject>();
    [SerializeField] List<int> itemsPrice = new List<int>();

    [Header("UI Objects")]
    public Text coinText;
    public Text highScoreText;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject ui;
    [SerializeField] Text buyButtonText;
    [SerializeField] Button buyButton;
    [SerializeField] Image buyButtonSpriteRenderer;

    [Header("Sounds")]
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip purchaseSound;

    [Header("Sprites")]
    [SerializeField] Sprite redSprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite greenSprite;

    [Header("Camera")]
    [SerializeField] Animator cameraAnimator;

    private int selectedItem = 0;
    private int currentSelectedItem = 0;

    private SkinData skinData = new SkinData();

    public static ShopManager singleton;

    private void Awake() => singleton = this;

    private void Start()
    {
        RefreshData();

        // Reset preview to selected skin
        foreach (GameObject model in skinModels)
            model.SetActive(false);
        skinModels[selectedItem].SetActive(true);
    }

    public void SelectItem(int itemIndex)
    {
        SoundManager.singleton.PlaySoundEffect(clickSound);
        if (currentSelectedItem == itemIndex) return;

        currentSelectedItem = itemIndex;

        // Update Preview Model
        foreach (GameObject model in skinModels)
            model.SetActive(false);
        skinModels[itemIndex].SetActive(true);

        UpdateBuyButton();
    }

    private void UpdateBuyButton()
    {
        if (skinData.ownedSkins.Contains(currentSelectedItem))
        {
            buyButtonSpriteRenderer.sprite = defaultSprite;
            buyButtonText.text = (currentSelectedItem == selectedItem) ? "Selected" : "Select";
            buyButton.enabled = true;
        }
        else
        {
            buyButtonText.text = itemsPrice[currentSelectedItem] + "$";
            buyButtonSpriteRenderer.sprite = 
               (currentGoldAmount >= itemsPrice[currentSelectedItem]) ? greenSprite : redSprite;
            buyButton.enabled = true;
        }
    }

    public void OnBuyClick()
    {
        if (skinData.ownedSkins.Contains(currentSelectedItem))
        {
            // Select
            SaveSelectedItem();
        }
        else
        {
            // Purchase
            if (currentGoldAmount >= itemsPrice[currentSelectedItem])
            {
                PurchaseItem();
                SoundManager.singleton.PlaySoundEffect(purchaseSound);
            }
            else
            {
                SoundManager.singleton.PlaySoundEffect(errorSound);
            }
        }
    }

    public void SaveSelectedItem()
    {
        selectedItem = currentSelectedItem;
        skinData.selectedSkin = selectedItem;

        SaveLoadManager.singleton.Save(skinData, SaveLoadManager.singleton.skinDataFileName);
        CloseShop();
    }

    public void PurchaseItem()
    {
        // Deduct money
        currentGoldAmount -= itemsPrice[currentSelectedItem];
        coinText.text = currentGoldAmount.ToString();

        // Add to owned skins
        skinData.ownedSkins.Add(currentSelectedItem);

        // Automatically select the purchased skin
        selectedItem = currentSelectedItem;
        skinData.selectedSkin = selectedItem;

        // Save both currency and skin data
        SaveLoadManager.singleton.Save(skinData, SaveLoadManager.singleton.skinDataFileName);

        // Update currency save
        CurrencyData currencyData = new CurrencyData();
        SaveLoadManager.singleton.Load(currencyData, SaveLoadManager.singleton.currencyDataFileName);
        currencyData.coinAmount = currentGoldAmount;
        SaveLoadManager.singleton.Save(currencyData, SaveLoadManager.singleton.currencyDataFileName);

        UpdateBuyButton();
    }

    public void OpenShop()
    {
        cameraAnimator.SetBool("IsShopOpen", true);
        SoundManager.singleton.PlaySoundEffect(clickSound);
        shopMenu.SetActive(true);
        ui.SetActive(false);

        // Refresh when opening
        RefreshData();
        SelectItem(selectedItem);
    }

    public void CloseShop()
    {
        cameraAnimator.SetBool("IsShopOpen", false);
        SoundManager.singleton.PlaySoundEffect(clickSound);
        shopMenu.SetActive(false);
        ui.SetActive(true);

        currentSelectedItem = selectedItem;

        // Reset preview to selected skin
        foreach (GameObject model in skinModels)
            model.SetActive(false);
        skinModels[selectedItem].SetActive(true);
    }

    public void RefreshData()
    {
        // Load Currency
        CurrencyData currencyData = new CurrencyData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
        {
            SaveLoadManager.singleton.Load(currencyData, SaveLoadManager.singleton.currencyDataFileName);
            currentGoldAmount = currencyData.coinAmount;
            coinText.text = currentGoldAmount.ToString();
            highScoreText.text = "High Score: " + currencyData.highScore;
        }
        else
        {
            SaveLoadManager.singleton.Save(currencyData, SaveLoadManager.singleton.currencyDataFileName);
        }

        // Load Skin Data
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.skinDataFileName))
        {
            SaveLoadManager.singleton.Load(skinData, SaveLoadManager.singleton.skinDataFileName);
        }
        else
        {
            SaveLoadManager.singleton.Save(skinData, SaveLoadManager.singleton.skinDataFileName);
        }

        selectedItem = skinData.selectedSkin;
        currentSelectedItem = selectedItem;
    }
}