using UnityEngine;

public class CustomButton : MonoBehaviour
{
    public AudioClip clickSound;

    public virtual void OnClick()
    {
        SoundManager.singleton.PlaySoundEffect(clickSound);
    } 
}
