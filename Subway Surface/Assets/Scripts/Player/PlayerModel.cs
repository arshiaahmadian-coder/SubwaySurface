using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public PlayerController playerController;

    public void PlayWalkSound1() {
        playerController.PlayWalkSound1();
    }

    public void PlayWalkSound2() {
        playerController.PlayWalkSound2();
    }
}
