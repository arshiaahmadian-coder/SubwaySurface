using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectClip;
    public float rotateSpeed;
    public GameObject coinModel;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            // play coin get sound effect
            SoundManager.singleton.PlaySoundEffect(collectClip);
            // play particle effect
            // give coin
            CoinCounter.singleton.AddCoin();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        coinModel.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
