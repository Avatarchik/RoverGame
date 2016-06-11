using UnityEngine;
using System.Collections;

public class Skidmarks : MonoBehaviour {

    public WheelCollider CorrespondingCollider;
    public GameObject skidMarkPrefab;

    private void Update()
    {
        Vector3 ColliderCenterPoint = CorrespondingCollider.transform.TransformPoint(CorrespondingCollider.center);
        RaycastHit[] hits = Physics.RaycastAll(ColliderCenterPoint, -CorrespondingCollider.transform.up, CorrespondingCollider.radius);

        if (hits.Length > 0)
            transform.position = hits[0].point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
        else
            transform.position = ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
    }
}
