using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class PuzzleMenu : Menu
    {
        public Button exitButton;

        [HideInInspector]
        public InteractiblePuzzle currentPuzzleObject = null;

        private PuzzleManager cachedPuzzleManager;


        public void Open(InteractiblePuzzle ip)
        {
            currentPuzzleObject = ip;
            base.Open();
        }


        public override void Close()
        {
            currentPuzzleObject.interactible = true;
            base.Close();
        }


        public PuzzleManager CachedPuzzleManager
        {
            get { return (cachedPuzzleManager != null) ? cachedPuzzleManager : cachedPuzzleManager = GameObject.FindObjectOfType<PuzzleManager>(); }
        }


        public void Exit()
        {
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
