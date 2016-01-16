using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Camera cam;
    public float shakeDecay = 0.4f;
    public float shakeIntensity = 2;

    private Vector3 originPosition;
    private Quaternion originRotation;


    public void Shake(float intensity, float decay)
    {
        originPosition = cam.transform.localPosition;
        originRotation = cam.transform.localRotation;

        StartCoroutine(ShakeCoroutine(intensity, decay));
    }

	private IEnumerator ShakeCoroutine(float intensity, float decay)
    {
        while (intensity > 0)
        {
            cam.transform.localPosition = originPosition + Random.insideUnitSphere * intensity * 0.1f;
            // cam.transform.rotation = new Quaternion(
            //     originRotation.x + Random.Range(-intensity, intensity) * 0.2f,
            //     originRotation.y + Random.Range(-intensity, intensity) * 0.2f,
            //     originRotation.z + Random.Range(-intensity, intensity) * 0.2f,
            //    originRotation.w + Random.Range(-intensity, intensity) * 0.2f);
            intensity -= decay;
            yield return null;
        }

        cam.transform.localPosition = originPosition;
        cam.transform.localRotation = originRotation;
    }
}
