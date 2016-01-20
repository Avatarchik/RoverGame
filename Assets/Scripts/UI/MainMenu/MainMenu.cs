using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : Menu
{
    public Button startGameButton;
    public Button loadGameButton;
    public Button optionsButton;
    public Button exitGameButton;


    public override void Open()
    {
        base.Open();
    }


    public override void Close()
    {
        base.Close();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void OpenOptions()
    {
        //nothing for now
    }


    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        exitGameButton.onClick.AddListener(QuitGame);
        optionsButton.onClick.AddListener(OpenOptions);
    }
}
