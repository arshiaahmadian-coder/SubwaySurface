using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranzision : MonoBehaviour
{
    public Animator animator;
    public int sceneIndex;
    public static SceneTranzision singleton;
    private void Awake() { singleton = this; }

    public void FadeIn(int i) {
        animator.SetTrigger("FadeIn");
        sceneIndex = i;
    }

    public void Change()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
