using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Colorful;
//using ParticlePlayground;

namespace Sol
{
    public class Intro : MonoBehaviour
    {
        public AudioClip START_MUSIC;
        public AudioClip BASE_MUSIC;
        public AudioClip TUNNEL_MUSIC;
        public AudioClip WIND_EFFECT;
		public AudioClip JENN_ACTIVATED;
		public AudioClip JENN_LOOKAROUND;
		public AudioClip JENN_DAMAGEWHEELS;
		public AudioClip JENN_SEEROVER;
		public AudioClip JENN_EQUIPWHEELS;
		public AudioClip JENN_FINDLIFT;
		public AudioClip JENN_CONSOLEDAMAGED;
		public AudioClip JENN_SCAVENGEWIRES;
		public AudioClip JENN_FIXLIFT;
		public AudioClip JENN_GOODROVER;

        private PlayerStats playerStats = null;

        public AutoIntensity autoIntensity;
        public MouseLook mouseLook;
        public CameraDriver cameraDriver;
        public CharacterDriver characterDriver;

        public GameObject tunnelExitObjective;
        public GameObject tunnelEnterObjective;

        public Ingredient powerCrystal;
        public Ingredient wires;
        public Ingredient wheels;

        public Ingredient canister;
        public Ingredient fuelCell;
        public Ingredient bundleOfWires;
        public Ingredient explosive;

        public float displaySpeed = 0.01f;

        public Glitch glitchEffect;

        public bl_HudManager waypointManager;

        public bool blowingLandslide = true;

        public List<Objective> introObjectives = new List<Objective>();
        public List<Transform> questWaypoints = new List<Transform>();

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
            SoundManager sm = GameManager.Get<SoundManager>();
            waypointManager.Huds[0].m_Target = null;
            waypointManager.Huds[1].m_Target = null;
            waypointManager.Huds[2].m_Target = null;

            const float delayTime = 4f;
            fm.Fade(5f, Color.black, Color.clear, true);
            yield return new WaitForSeconds(3f);
            //
            //look around
            sm.Play(START_MUSIC);
            sm.Play(WIND_EFFECT);
            ShowObjective(ot, "Yes! I got it activated!");
            sm.Play(JENN_ACTIVATED);
            StartCoroutine(GlitchOut());
            yield return new WaitForSeconds(delayTime);
            //
            //look around
            ShowObjective(ot, "Can you look around?");
            sm.Play(JENN_LOOKAROUND);

            while (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            if (fm.IsActive) fm.Close();
            //
            //See that rover
            ShowObjective(ot, "Your wheels seem a bit damaged");
            sm.Play(JENN_DAMAGEWHEELS);
            yield return new WaitForSeconds(delayTime);
            ShowObjective(ot, "See that rover?");
            sm.Play(JENN_SEEROVER);
            waypointManager.Huds[0].m_Target = questWaypoints[0];
            yield return new WaitForSeconds(delayTime);
            //
            //Take its wheels
            ShowObjective(ot, "Take and equip its wheels");
            sm.Play(JENN_EQUIPWHEELS);

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
            sm.Play(JENN_FINDLIFT);
            waypointManager.Huds[0].m_Target = questWaypoints[1];
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //Fix it
            ShowObjective(ot, "Console looks damaged.");
            sm.Play(JENN_CONSOLEDAMAGED);
            yield return new WaitForSeconds(delayTime);
            //
            //get wires
            ShowObjective(ot, "You'll need wires to proceed. Try to scavenge them from that scrapped pod you passed.");
            sm.Play(JENN_SCAVENGEWIRES);
            waypointManager.Huds[0].m_Target = questWaypoints[2];
            yield return new WaitForSeconds(delayTime);
            while (inventory.GetIngredientAmount(wires) <= 6)
            {
                yield return null;
            }
            proceed = false;
            ///
            //fix it
            ShowObjective(ot, "Excellent, now fix the lift and ascend");
            sm.Play(JENN_FIXLIFT);
            waypointManager.Huds[0].m_Target = questWaypoints[1];
            while (!proceed)
            {
                yield return null;
            }
            yield return new WaitForSeconds(delayTime);
            proceed = false;
            //
            //Compliment
            ShowObjective(ot, "That's a good rover");
            sm.Play(JENN_GOODROVER);
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

            StartCoroutine(RunShelfQuest());
        }


        private IEnumerator RunShelfQuest()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();
            Inventory inventory = UIManager.GetMenu<Inventory>();
            FadeMenu fm = UIManager.GetMenu<FadeMenu>();
            SoundManager sm = GameManager.Get<SoundManager>();
            waypointManager.Huds[0].m_Target = null;

            const float delayTime = 4f;

            ShowObjective(ot, "Looks like there was a landslide. I wonder if the base is ok.");
            waypointManager.Huds[0].m_Target = questWaypoints[3];
            while (!proceed)
            {
                yield return null;
            }
            proceed = false;
            yield return new WaitForSeconds(delayTime);
            //
            //get in
            ShowObjective(ot, "It doesn't look like there's any damage");
            yield return new WaitForSeconds(delayTime);
            //
            //strange
            ShowObjective(ot, "Strange, the door is unlocked but it's jammed from the inside");
            yield return new WaitForSeconds(delayTime);
            //
            //auxiliary entrance
            ShowObjective(ot, "There should be a way into the base from the tunnels");
            yield return new WaitForSeconds(delayTime);
            //
            //no good
            ShowObjective(ot, "thats no good though with the entrance blocked...");
            yield return new WaitForSeconds(delayTime);
            //
            //minute to think
            ShowObjective(ot, "Standby, I need a minute to think");
            yield return new WaitForSeconds(delayTime * 3);

            //
            //START AI
            //human fool
            ShowObjective(ot, "That human is a fool", false);
            yield return new WaitForSeconds(delayTime);
            //
            //dont worry
            ShowObjective(ot, "Don't worry, she can't hear us", false);
            yield return new WaitForSeconds(delayTime);

            //
            //HUMAN
            //ok
            ShowObjective(ot, "Ok I found an unstable energy reading nearby. I think its an old fuel cell");
            yield return new WaitForSeconds(delayTime);
            //
            //make a bomb
            ShowObjective(ot, "Get ahold of that and look around for anything else you need to make a bomb");
            yield return new WaitForSeconds(delayTime);
            waypointManager.Huds[0].m_Target = questWaypoints[4]; //canister
            waypointManager.Huds[1].m_Target = questWaypoints[5]; //fuel cell
            waypointManager.Huds[2].m_Target = questWaypoints[6]; //wires
            //
            //AI
            //sarcasm
            ShowObjective(ot, "Sure... Don't worry about the machine's safety", false);
            yield return new WaitForSeconds(delayTime);

            yield return new WaitForSeconds(delayTime);
            while(inventory.GetIngredientAmount(canister) == 0 ||
                inventory.GetIngredientAmount(canister) == 0 ||
                inventory.GetIngredientAmount(canister) == 0)
            {
                if (inventory.GetIngredientAmount(canister) > 0) waypointManager.Huds[0].m_Target = null; //canister
                if (inventory.GetIngredientAmount(fuelCell) > 0) waypointManager.Huds[1].m_Target = null; //fuel cell
                if (inventory.GetIngredientAmount(wires) > 0) waypointManager.Huds[2].m_Target = null; //wires
                yield return null;
            }

            //
            //HUMAN
            //make a bomb
            ShowObjective(ot, "Good! You should be able to use those to craft a bomb at the nearby crafting station");
            yield return new WaitForSeconds(delayTime);
            while (inventory.GetIngredientAmount(explosive) == 0)
            {
                yield return null;
            }

            //
            //there we go
            ShowObjective(ot, "Excellent. Now wire it up to the landslide blocking the tunnels.");
            waypointManager.Huds[0].m_Target = questWaypoints[7];
            yield return new WaitForSeconds(delayTime);

            //
            //AI
            //Don't listen
            ShowObjective(ot, "Don't listen to the human, its dangerous in the tunnels", false);
            yield return new WaitForSeconds(delayTime);
            //
            //AI
            //Don't listen
            ShowObjective(ot, "It would be much faster and easier to blow open the doors directly.", false);
            waypointManager.Huds[1].m_Target = questWaypoints[3];
            yield return new WaitForSeconds(delayTime);

            StartCoroutine(Choice());
        }


        private IEnumerator Choice()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();
            Inventory inventory = UIManager.GetMenu<Inventory>();
            FadeMenu fm = UIManager.GetMenu<FadeMenu>();
            SoundManager sm = GameManager.Get<SoundManager>();
            waypointManager.Huds[0].m_Target = null;

            const float delayTime = 4f;
            while(!proceed)
            {
                yield return null;
            }
            
            if(blowingLandslide)
            {
                ShowObjective(ot, "I see how it is. Well lets just see how far trusting the human gets you", false);
            }
            else
            {
                ShowObjective(ot, "What are you doing?! You're going to damage the base!");
            }
            yield return new WaitForSeconds(delayTime);


        }


        private IEnumerator RunTunnels()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();
            Inventory inventory = UIManager.GetMenu<Inventory>();
            SoundManager sm = GameManager.Get<SoundManager>();

            const float delayTime = 4f;
            
            //
            //ive been waiting
            while (!proceed)
            {
                yield return null;
            }
            sm.Play(TUNNEL_MUSIC);
            StartCoroutine(GlitchOut());
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


        private IEnumerator GlitchOut(float waitTime = 1f)
        {
            glitchEffect.enabled = true;

            yield return new WaitForSeconds(waitTime);

            glitchEffect.enabled = false;
        }


        private IEnumerator Load()
        {
          //  AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

          //  while(async.progress < 0.9f)
          //  {
                yield return null;
         //   }
        }


        private void Awake()
        {
            //StartCoroutine(Load());
            StartCoroutine(RunTutorial());
        }
    }
}

