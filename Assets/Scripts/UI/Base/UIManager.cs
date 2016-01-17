using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static FadeMenu FadeMenuInstance;
    public static MessageMenu MessageMenuInstance;

    public FadeMenu fadeMenu;
    public MessageMenu messageMenu;

    private void Awake ()
    {
        FadeMenuInstance = fadeMenu;
        MessageMenuInstance = messageMenu;
	}
}
