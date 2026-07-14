using UnityEngine;

public class RestartButton : CustomButton
{
    public override void OnClick()
    {
        base.OnClick();
        SceneTranzision.singleton.FadeIn(1);
    }
}
