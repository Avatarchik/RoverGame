using UnityEngine;
using UnityEngine.SceneManagement;
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

        public GameObject tunnelExitObjective;
        public GameObject tunnelEnterObjective;

        public Ingredient powerCrystal;
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
            const float delayTime = 4f;
            fm.Fade(5f, Color.black, Color.clear, true);
            yield return new WaitForSeconds(3f);
            //
            //look around
            ShowObjective(ot, "Yes! I got it activated!");
            yield return new WaitForSeconds(delayTime);
            //
            //look around
            ShowObjective(ot, "Can you look around?");

            while (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            if (fm.IsActive) fm.Close();
            //
            //See that rover
            ShowObjective(ot, "Your wheels seem a bit damaged");
            yield return new WaitForSeconds(delayTime);
            ShowObjective(ot, "See that rover?");
            yield return new WaitForSeconds(delayTime);
            //
            //Take its wheels
            ShowObjective(ot, "Take and equip its wheels");

            while (inventory.GetIngredientAmount(wheels) == 0)
            {
                yield return null;
            }
            while (inventory.GetIngredientAmount(wheels) != 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            //
            //Find the lift
            ShowObjective(ot, "Good, now find the lift");

            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //Fix it
            ShowObjective(ot, "Console looks damaged. Fix it and ascend");

            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //Compliment
            ShowObjective(ot, "That's a good rover");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //exposition
            ShowObjective(ot, "My boss told me not to power you on");
            yield return new WaitForSeconds(delayTime);
            ShowObjective(ot, "But I need to find out what happened to my friends on the planet.");
            yield return new WaitForSeconds(delayTime);
            //
            //go to the dome
            ShowObjective(ot, "Looks like there was a landslide. I wonder if the base is ok.");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //get in
            ShowObjective(ot, "Get inside");
            while (!proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            proceed = false;
            //
            //get in
            ShowObjective(ot, "Well Done!");
            while (!proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            proceed = false;
            //
            //get in
            ShowObjective(ot, "Damn, the teleporter is busted...");
            tunnelEnterObjective.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            //
            //get into the caves
            ShowObjective(ot, "Go back outside and look for a way into tunnels");
            while (!proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            proceed = false;
            //
            //you need explosives
            ShowObjective(ot, "You'll need explosives to clear this landslide");
            yield return new WaitForSeconds(delayTime);
            //
            //search
            ShowObjective(ot, "Look around for a bundle of wires, a canister, and an active fuel cell to make a bomb");
            yield return new WaitForSeconds(delayTime);
            //
            //i need to go
            ShowObjective(ot, "My boss is calling so I need to go. You're on your own for now");
            yield return new WaitForSeconds(delayTime);
            //
            //good luck
            ShowObjective(ot, "Good luck little rover!");
            yield return new WaitForSeconds(delayTime);

            StartCoroutine(RunTunnels());
        }


        private IEnumerator RunTunnels()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();
            Inventory inventory = UIManager.GetMenu<Inventory>();
            const float delayTime = 4f;
            
            //
            //ive been waiting
            while (!proceed)
            {
                yield return null;
            }
            ShowObjective(ot, "I've been waiting for you", false);
            yield return new WaitForSeconds(delayTime);
            //
            //cant always trust
            ShowObjective(ot, "You can't always trust what the voices in your head say", false);
            yield return new WaitForSeconds(delayTime);
            //
            //trust me
            ShowObjective(ot, "You can trust me though", false);
            yield return new WaitForSeconds(delayTime);
            //
            //find the crystal
            ShowObjective(ot, "Find the power crystal for the teleporter", false);
            yield return new WaitForSeconds(delayTime);
            //
            //in here somewhere
            ShowObjective(ot, "It's in here somewhere", false);
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //take it
            ShowObjective(ot, "Take it, and go back to the teleporter", false);
            while (inventory.GetIngredientAmount(powerCrystal) == 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            //
            //they are back
            ShowObjective(ot, "Looks like your human is back", false);
            yield return new WaitForSeconds(delayTime);
            //
            //they are back
            ShowObjective(ot, "Its been lonely since I lost contact with them", false);
            yield return new WaitForSeconds(delayTime);
            proceed = false;
            //
            //they are back
            ShowObjective(ot, "Now go back, I'll be watching", false);
            tunnelExitObjective.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            //
            //you came back
            ShowObjective(ot, "You came back! and you have the power crystal");
            yield return new WaitForSeconds(delayTime);
            //
            //you came back
            ShowObjective(ot, "I was worried when I came back and there was no signal");
            yield return new WaitForSeconds(delayTime);
            //
            //plug it into the teleporter
            ShowObjective(ot, "Plug it into the teleporter and lets get you out of that crater.");
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            //
            //plug it into the teleporter
            ShowObjective(ot, "You're going to be the one to find the missing colonists");
        }


        private void ShowObjective(ObjectiveTracker ot, string message, bool admin = true)
        {
            ot.Open(message, admin);
            StartCoroutine(DelayedClose(ot));
        }


        private IEnumerator DelayedClose(ObjectiveTracker ot)
        {
            yield return new WaitForSeconds(3.5f);
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
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
           StartCoroutine(RunTutorial());
        }
    }
}

