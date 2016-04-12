using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Intro : MonoBehaviour
    {
        private PlayerStats playerStats = null;

        public AutoIntensity autoIntensity;
        public MouseLook mouseLook;
        public CameraDriver cameraDriver;
        public CharacterDriver characterDriver;

        public Ingredient wheels;

        public float displaySpeed = 0.01f;

        public List<Objective> introObjectives = new List<Objective>();

        private bool proceed = false;

        public PlayerStats CachedPlayerStats
        {
            get { return (playerStats != null) ? playerStats : playerStats = GameObject.FindObjectOfType<PlayerStats>(); }
        }


        public void Next()
        {
            proceed = true;
        }

        private IEnumerator RunTutorial()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();
            Inventory inventory = UIManager.GetMenu<Inventory>();
            FadeMenu fm = UIManager.GetMenu<FadeMenu>();

            fm.Fade(5f, Color.black, Color.clear, true);
            yield return new WaitForSeconds(3f);
            //look around
            ShowObjective(ot, "Look around");

            while (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(6f);
            if (fm.IsActive) fm.Close();
            //
            //move around
            ShowObjective(ot, "Explore the area");

            while (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || proceed)
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(6f);
            //
            //See that rover
            Debug.Log(1);
            ShowObjective(ot, "See that rover");
            yield return new WaitForSeconds(6f);
            //
            //Take its wheels
            Debug.Log(1);
            ShowObjective(ot, "Take its wheels");

            while (inventory.GetIngredientAmount(wheels) == 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(6f);
            //
            //Equip them
            Debug.Log(2);
            ShowObjective(ot, "Equip them");
            yield return new WaitForSeconds(6f);
            //
            //Find the lift
            Debug.Log(1);
            ShowObjective(ot, "Find the lift");

            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //Fix it
            Debug.Log(2);
            ShowObjective(ot, "Fix it");

            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //Ascend
            Debug.Log(3);
            ShowObjective(ot, "Ascend");
            while(!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //Compliment
            Debug.Log(4);
            ShowObjective(ot, "That's a good rover");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //go to the dome
            Debug.Log(5);
            ShowObjective(ot, "Go to the dome");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //get in
            Debug.Log(6);
            ShowObjective(ot, "Get in");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //find the teleporter
            Debug.Log(7);
            ShowObjective(ot, "Find the teleporter");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(6f);
            //
            //fix teleporter
            Debug.Log(8);
            ShowObjective(ot, "Fix this too");
            while (!proceed)
            {
                yield return null;
            }
        }


        private void ShowObjective(ObjectiveTracker ot, string message)
        {
            ot.Open(message);
            StartCoroutine(DelayedClose(ot));
        }


        private IEnumerator DelayedClose(ObjectiveTracker ot)
        {
            yield return new WaitForSeconds(5f);
            ot.Close();
        }


        private IEnumerator DisplaySubObjectives()
        {
            yield return null;
        }

        //TODO dont rely on this
        private IEnumerator AudioFade(AudioSource source)
        {
            float elapsedTime = 0f;
            float desiredTime = 15f;

            while(elapsedTime < desiredTime)
            {
                if (source == null) break;
                source.volume = Mathf.Lerp(0, 1, elapsedTime / desiredTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
        }


        private void Awake()
        {
           StartCoroutine(RunTutorial());
        }
    }
}

