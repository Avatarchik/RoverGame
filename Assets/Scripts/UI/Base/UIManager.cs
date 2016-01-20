using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static PlayerStats PlayerStatsInstance;
    public static FadeMenu FadeMenuInstance;
    public static MessageMenu MessageMenuInstance;

    public PlayerStats playerStats;
    public FadeMenu fadeMenu;
    public MessageMenu messageMenu;

    private void Awake ()
    {
        PlayerStatsInstance = playerStats;
        FadeMenuInstance = fadeMenu;
        MessageMenuInstance = messageMenu;
	}
}
