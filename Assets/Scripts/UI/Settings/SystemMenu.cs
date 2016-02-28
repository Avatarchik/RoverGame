using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class SystemMenu : Menu
    {
        public Button saveGamebutton;
        public Button loadGameButton;
        public Button controlsButton;
        public Button graphicsButton;
        public Button audioButton;
        public Button exitGameButton;


        public void SaveGame()
        {
            //TODO implement save game
        }


        public void LoadGame()
        {
            //TODO implement load game
        }


        public void OpenControls()
        {
            //TODO implement controls
        }


        public void OpenGraphics()
        {
            //TODO implement settings menu
        }


        public void OpenAudio()
        {
            //TODO implement audio management
        }


        public void ExitGame()
        {
            Application.Quit();
        }


        private void Awake()
        {
            saveGamebutton.onClick.AddListener(SaveGame);
            loadGameButton.onClick.AddListener(LoadGame);
            controlsButton.onClick.AddListener(OpenControls);
            graphicsButton.onClick.AddListener(OpenGraphics);
            audioButton.onClick.AddListener(OpenAudio);
            exitGameButton.onClick.AddListener(ExitGame);
        }
    }

}
