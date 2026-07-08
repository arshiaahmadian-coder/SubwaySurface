using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager singleton;
    private void Awake() { singleton = this; }

    public float swipeThreshold = 100f;
}
