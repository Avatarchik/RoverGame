using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class InteractiblePuzzle : InteractibleObject
    {
		public delegate void PuzzleComplete ();
		public static event PuzzleComplete onPuzzleComplete;

        public Object missionObject;
        public Object levelObject;
        public UIEvents uiEvents;

        public PuzzleManager puzzleManager;
		public GameObject myPuzzleCanvas;
		private bool firstInteraction = true;
		public bool puzzleScaled = false;
		private bool checkEntered;
		private bool checkScaled;
		private bool checkRunner;

        public string message;

        public List<GameObject> objectsToActivate = new List<GameObject>();
        public List<GameObject> objectsToDeactivate = new List<GameObject>();
		public List<Collider> collidersToDeactivate = new List<Collider>();
        public List<InteractibleObject> objectsToTrigger = new List<InteractibleObject>();

        public bool questTrigger = false;

		protected bool complete = false;

        public virtual bool Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                if(complete)
                {

                    foreach(GameObject go in objectsToActivate)
                    {
                        go.SetActive(true);
                    }

                    foreach (GameObject go in objectsToDeactivate)
                    {
                        go.SetActive(false);
                    }

					foreach (Collider co in collidersToDeactivate)
					{
						co.enabled = false;
					}

                    foreach(InteractibleObject io in objectsToTrigger)
                    {
                        io.Interact();
                    }
					interactible = false;
					myPuzzleCanvas.GetComponent<PuzzleAnimHandler> ().BlinkLight ();
					onPuzzleComplete ();
                }
            }
        }

        public UIEvents UiEvents
        {
            get { return (uiEvents != null) ? uiEvents : uiEvents = GameObject.FindObjectOfType<UIEvents>(); }
        }

        public override void Interact()
        {
			print ("interact");
			if(interactible)
            {
				print ("YEPP");
				base.Interact ();
				print ("interact");
				UiEvents.MissionButtonEvent (missionObject);
				UiEvents.LevelButtonEvent (levelObject);
				print ("buttons");
				checkScaled = true;
				checkEntered = true;
				print ("checks");
				if (firstInteraction) {
					print ("first");
					myPuzzleCanvas.GetComponent<Animator> ().SetTrigger ("FadeForward");
					myPuzzleCanvas.GetComponent<PuzzleAnimHandler> ().ActivateLight ();
					firstInteraction = false;
					print ("done");
				} else if (!puzzleScaled) {
						myPuzzleCanvas.GetComponent<Animator> ().SetTrigger ("FadeForward");
					}
				interactible = false;
            }
        }

		public void InitiatePuzzle(){
			PuzzleMenu pu = UIManager.GetMenu<PuzzleMenu> ();
			pu.Open (this);
			if (myPuzzleCanvas.GetComponent<RotateToObject> () != null) {
				myPuzzleCanvas.GetComponent<RotateToObject> ().enabled = true;
			}
			Camera.main.GetComponentInParent<RotateToObject> ().RotateTo (myPuzzleCanvas.transform);
			checkRunner = true;
			UIManager.Close<MessageMenu> ();
		}

		public void Update(){
			if (checkEntered) {
				if (transform.GetComponentInChildren<PuzzleTriggerZone>().inPuzzleZone) {
					InitiatePuzzle ();
					checkEntered = false;
				}
			}
			if (checkScaled) {
				if (puzzleScaled) {
					puzzleManager.InitializePuzzle (myPuzzleCanvas);
					checkScaled = false;
				}
			}
			if (checkRunner) {
				if (puzzleScaled) {
					puzzleManager.RunPuzzle ();
					checkRunner = false;
				}
			}
		}
    }
}