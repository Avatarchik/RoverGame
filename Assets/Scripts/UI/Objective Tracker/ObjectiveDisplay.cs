using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplay : MonoBehaviour
{
    public Text permanentText;
    public Text objectiveText;

    public Objective objective;


    private void Initialize()
    {
        permanentText.text = objective.permanentText;
        objectiveText.text = objective.objectiveText;
    }
}
