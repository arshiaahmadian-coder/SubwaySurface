using UnityEngine;

public class StartButton : CustomButton
{
    public override void OnClick()
    {
        base.OnClick();
        // play start sound
        SceneTranzision.singleton.FadeIn(1);
        SoundManager.singleton.StopMusicSlowly(0.3f);
    }
}
