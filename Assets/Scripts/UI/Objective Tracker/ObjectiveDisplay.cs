using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplay : MonoBehaviour
{
    public Text permanentText;
    public Text objectiveText;

    public Objective objective;

    private bool isFilling = true;
    public float fillSpeed = 0.05f;
    private string filledString = "";

    public bool Isfilling
    {
        get { return isFilling; }
    }

    public IEnumerator FillText(string s)
    {
        isFilling = true;
        char[] chars = s.ToCharArray();

        for(int i = 0; i < chars.Length; i++)
        {
            yield return new WaitForSeconds(fillSpeed);
            filledString = filledString + chars[i];
            objectiveText.text = filledString;
        }

        isFilling = false;
    }

    public void Initialize()
    {
        StartCoroutine(FillText(objective.objectiveText));
    }

    /// <summary>
    /// type speed refers to wait time before each char is typed
    /// </summary>
    /// <param name="typeSpeed"></param>
    public void Initialize(float typeSpeed)
    {
        fillSpeed = typeSpeed;
        StartCoroutine(FillText(objective.objectiveText));
    }
}
