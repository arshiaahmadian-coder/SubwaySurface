using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : CustomButton
{
    public override void OnClick()
    {
        base.OnClick();
        SceneTranzision.singleton.FadeIn(0);
    }
}
