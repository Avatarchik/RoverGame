using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveTracker : Menu
{
    public ObjectiveDisplay objectivePrefab;
    public Transform objectiveContainer;

    public List<ObjectiveDisplay> displayedObjectives = new List<ObjectiveDisplay>();


    public ObjectiveDisplay AddObjective(Objective objective)
    {
        if (!IsActive) Open();

        ObjectiveDisplay od = Instantiate(objectivePrefab);
        od.transform.SetParent(objectiveContainer, false);
        od.objective = objective;
        od.Initialize();
        displayedObjectives.Add(od);

        return od;
    }
}
