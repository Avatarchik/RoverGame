using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class PuzzleMenu : Menu
    {
        public Button exitButton;
        public Text messageText;

        [HideInInspector]
        public InteractiblePuzzle currentPuzzleObject = null;

        private PuzzleManager cachedPuzzleManager;

        public PuzzleManager CachedPuzzleManager
        {
            get { return (cachedPuzzleManager != null) ? cachedPuzzleManager : cachedPuzzleManager = GameObject.FindObjectOfType<PuzzleManager>(); }
        }


        public void Open(InteractiblePuzzle ip)
        {
            messageText.text = ip.message;
            currentPuzzleObject = ip;
            base.Open();
        }


        public override void Close()
        {
            Debug.Log("close");

            currentPuzzleObject.UiEvents.MissionButtonEvent(null);
            currentPuzzleObject.UiEvents.LevelButtonEvent(null);

            currentPuzzleObject.interactible = true;
            base.Close();
        }


        public void Close(bool completed)
        {
            if(completed)
            {
                currentPuzzleObject.Complete = true;
                base.Close();
            }
            else
            {
                base.Close();
            }
        }


        public void Exit()
        {
            Debug.Log("exit");
            currentPuzzleObject.interactible = true;
            CachedPuzzleManager.Close();
            Close();
        }


        public void Awake()
        {
            exitButton.onClick.AddListener(Exit);
        }
    }
}
