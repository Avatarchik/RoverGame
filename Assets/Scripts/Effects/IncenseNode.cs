using UnityEngine;
using System.Collections;

public class IncenseNode : MonoBehaviour
{
    public void Stop()
    {
        StopAllCoroutines();
    }


    public IEnumerator Travel(float desiredTime, Vector3 desiredPosition)
    {
        float elapsedTime = 0f;

        while(elapsedTime < desiredTime)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, elapsedTime / desiredTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
