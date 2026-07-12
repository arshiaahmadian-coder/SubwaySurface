using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake singleton;

    [SerializeField] CinemachineCamera cam;

    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        singleton = this;

        noise = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(intensity, duration));
    }

    IEnumerator ShakeRoutine(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0;
    }
}