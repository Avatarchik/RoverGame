using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject doorModel;

    public Transform closedPosition;
    public Transform openPosition;

    public float moveTime = 2f;


    private bool isOpen = false;

    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            if(isOpen != value)
            {
                isOpen = value;

                if (isOpen)
                {
                    StartCoroutine(MoveDoor(closedPosition, openPosition));
                }
                else
                {
                    StartCoroutine(MoveDoor(openPosition, closedPosition));
                }
            }
        }
    }


    private IEnumerator MoveDoor(Transform start, Transform end)
    {
        float elapsedTime = 0f;

        while(elapsedTime <= moveTime)
        {
            doorModel.transform.position = Vector3.Lerp(start.position, end.position, elapsedTime/moveTime);
            doorModel.transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, elapsedTime/moveTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
