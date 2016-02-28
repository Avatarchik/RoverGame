using UnityEngine;
using System.Collections;

public class Log : ScriptableObject
{
    public int id = -1;
    public string header = "";
    public string content = "";

    public string author = "";


    protected virtual void Initialize()
    {
        content = content.Replace("\\n", "\n");
    }


    protected virtual void Start()
    {
        Initialize();
    }
}
