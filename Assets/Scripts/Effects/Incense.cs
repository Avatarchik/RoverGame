using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Incense : MonoBehaviour
{
    public LineRenderer smokeLine;
    public IncenseNode nodePrefab;
    public float emissionDelay = 0.1f;
    public AnimationCurve trailThickness;
    public Transform spawnPos;

    public float fadeTime = 1f;
    public float driftTime = 2f;
    public Vector2 xDrift = new Vector2(-1f, 1f);
    public Vector2 yDrift = new Vector2(3f, 4f);
    public Vector2 zDrift = new Vector2(-1f, 1f);

    private List<IncenseNode> activeNodes = new List<IncenseNode>();
    private List<IncenseNode> inactiveNodes = new List<IncenseNode>();

	
    public IncenseNode GetNode()
    {
        IncenseNode newNode = null;

        if(inactiveNodes.Count > 0)
        {
            newNode = inactiveNodes[0];
            activeNodes.Add(newNode);
            inactiveNodes.RemoveAt(0);
        }
        else
        {
            newNode = Instantiate(nodePrefab, spawnPos.position, Quaternion.identity) as IncenseNode;
            activeNodes.Add(newNode);
        }

        return newNode;
    }


    private void InitializeIncenseNode(IncenseNode node)
    {
        node.transform.SetParent(transform);
        node.transform.position = spawnPos.position;
        node.gameObject.SetActive(true);

        Vector3 driftTarget = new Vector3(
            node.transform.position.x + Random.Range(xDrift.x, xDrift.y),
             node.transform.position.y + Random.Range(yDrift.x, yDrift.y),
             node.transform.position.z + Random.Range(zDrift.x, zDrift.y));
        node.StartCoroutine(node.Travel(driftTime, driftTarget));

        StartCoroutine(RecycleNode(node, fadeTime));
    }


    private IEnumerator NodeRequestLoop()
    {
        yield return new WaitForSeconds(emissionDelay);
        InitializeIncenseNode(GetNode());
        StartCoroutine(NodeRequestLoop());
    }


    private IEnumerator RecycleNode(IncenseNode node, float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < activeNodes.Count; i++)
        {
            if (activeNodes[i].GetInstanceID() == node.GetInstanceID())
            {
                activeNodes.RemoveAt(i);
                break;
            }
        }

        inactiveNodes.Add(node);
        node.Stop();
        node.gameObject.SetActive(false);
    }


    private void Update()
    {
        UpdateTrailRenderer();
    }


    private void UpdateTrailRenderer()
    {
        Vector3[] positions = new Vector3[activeNodes.Count + 1];
       
        for(int i = 0; i < activeNodes.Count; i++)
        {
            positions[i] = activeNodes[i].transform.position;
        }
        positions[activeNodes.Count] = spawnPos.position;

        smokeLine.SetPositions(positions);
    }


    private void Awake()
    {
        StartCoroutine(NodeRequestLoop());
    }
}
