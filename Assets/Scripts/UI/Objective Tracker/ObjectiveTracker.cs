using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveTracker : Menu
{
    public ObjectiveDisplay objectivePrefab;
    public Transform objectiveContainer;

    public List<ObjectiveDisplay> displayedObjectives = new List<ObjectiveDisplay>();


    public override void Open()
    {
        StopAllCoroutines();
        StartCoroutine(DelayedClose());

        base.Open();
    }


    public ObjectiveDisplay AddObjective(Objective objective, float displaySpeed = 0f)
    {
        if (!IsActive) Open();

        StopAllCoroutines();
        StartCoroutine(DelayedClose());

        ObjectiveDisplay od = Instantiate(objectivePrefab);
        od.transform.SetParent(objectiveContainer, false);
        od.objective = objective;
        od.Initialize(displaySpeed);
        displayedObjectives.Add(od);

        return od;
    }


    private IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(10f);
        Close();
    }
}
