using UnityEngine;
using System.Collections;

public class Log : ScriptableObject
{
    public int id = -1;
    public string header = "";

    [TextArea(3, 10)]
    public string content = "";

    public string author = "";
}
