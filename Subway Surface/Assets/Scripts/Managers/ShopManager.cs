using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;

    [Header("UI Objects")]
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject ui;

    public void SaveSelectedItem() {
        SoundManager.singleton.PlaySoundEffect(clickSound);
    }

    public void SelectItem() {
        SoundManager.singleton.PlaySoundEffect(clickSound);
    }

    public void PurchaseItem() {
        SoundManager.singleton.PlaySoundEffect(clickSound);
    }

    public void OpenShop() {
        SoundManager.singleton.PlaySoundEffect(clickSound);
        shopMenu.SetActive(true);
        ui.SetActive(false);
    }

    public void CloseShop() {
        SoundManager.singleton.PlaySoundEffect(clickSound);
        shopMenu.SetActive(false);
        ui.SetActive(true);
    }
}
