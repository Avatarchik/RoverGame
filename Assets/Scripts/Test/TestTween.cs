using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    public GameObject playerCamera;
    public float travelSpeed;
    public Transform lookPoint;

    public Transform[] cameraPath = new Transform[0];


    private void Travel()
    {
        Vector3[] path = new Vector3[cameraPath.Length];

        for(int i = 0; i < cameraPath.Length; i++)
        {
            path[i] = cameraPath[i].position;
        }

        float distance = Vector3.Distance(playerCamera.transform.position, path[0]);
        for (int i = 1; i < path.Length; i++)
        {
            distance += Vector3.Distance(path[i - 1], path[i]);
        }

        playerCamera.transform.DOPath(path, distance / travelSpeed, PathType.CatmullRom, PathMode.Full3D).SetLookAt(lookPoint);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Travel();
        }
    }


    private void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }
}
