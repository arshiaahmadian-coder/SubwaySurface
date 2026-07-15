using UnityEngine;

public class CustomButton : MonoBehaviour
{

    public virtual void OnClick()
    {
        SoundManager.singleton.PlayClickSound();
    } 
}
