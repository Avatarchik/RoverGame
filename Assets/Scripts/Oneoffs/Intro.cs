using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Intro : MonoBehaviour
    {
        private const int VOICE_ID_1 = 2010;
        private const int VOICE_ID_2 = 2011;
        private const int VOICE_ID_3 = 2012;
        private const int MUSIC_ID_1 = 40;
        private const int SFX_ID_1 = 60;

        private PlayerStats playerStats = null;

        public AutoIntensity autoIntensity;
        public MouseLook mouseLook;
        public CameraDriver cameraDriver;
        public CharacterDriver characterDriver;
        public InteractibleObject safe;

        public Ingredient explosiveDevice;

        public Ingredient cannister;
        public Ingredient unstableFuelCell;
        public Ingredient bundleOfWires;

        public Objective pressAnyKeyObjective;
        public Objective lookObjective;
        public Objective moveObjective;
        public Objective escapePodObjective;
        public Objective constructExplosiveObjective;
        public Objective constructExplosiveSub1;
        public Objective constructExplosiveSub2;
        public Objective constructExplosiveSub3;
        public Objective craftExplosive;
        public Objective clearLandslideObjective;
        public Objective enterTunnelsObjective;

        public float displaySpeed = 0.01f;

        public List<Objective> introObjectives = new List<Objective>();
        

        public PlayerStats CachedPlayerStats
        {
            get { return (playerStats != null) ? playerStats : playerStats = GameObject.FindObjectOfType<PlayerStats>(); }
        }

        private IEnumerator RunTutorial()
        {
              FadeMenu fadeMenu = UIManager.GetMenu<FadeMenu>();
              ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();
              ObjectiveDisplay od;
              SoundManager soundManager = GameManager.Get<SoundManager>();
              Inventory inventory = UIManager.GetMenu<Inventory>();

              CachedPlayerStats.DisableMovement();

              //Press any key!!
              fadeMenu.Fade(0f, Color.clear, Color.black);
              bool anyKeyPressed = false;
              od = objectiveTracker.AddObjective(pressAnyKeyObjective, displaySpeed);
              while (!anyKeyPressed || od.Isfilling)
              {
                  anyKeyPressed = Input.anyKey;
                  yield return null;
              }

              //start playing sound and run through the console
              StartCoroutine(AudioFade(soundManager.Play(VOICE_ID_1, 1).GetComponent<AudioSource>()));

              yield return new WaitForSeconds(1f);

              for (int i = 0; i < introObjectives.Count; i++)
              {
                  yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
                  od = objectiveTracker.AddObjective(introObjectives[i], displaySpeed);
                  while(od.Isfilling)
                  {
                      yield return null;
                  }
              }

              fadeMenu.Fade(2f, Color.black, Color.clear);
              yield return new WaitForSeconds(4f);
              fadeMenu.Close();

              float prevMoveSpeedMultiplier = characterDriver.movementSpeedMultiplier;
              characterDriver.movementSpeedMultiplier = 0f;

              //look around and move around
              soundManager.Play(VOICE_ID_2);
              yield return new WaitForSeconds(2f);
              od = objectiveTracker.AddObjective(lookObjective, displaySpeed);
              while(od.Isfilling)
              {
                  yield return null;
              }
              //playerStats.EnableMovement();

              while(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
              {
                  yield return null;
              }

              yield return new WaitForSeconds(3f);

              od = objectiveTracker.AddObjective(moveObjective, displaySpeed);
              while (od.Isfilling)
              {
                  yield return null;
              }
              characterDriver.movementSpeedMultiplier = prevMoveSpeedMultiplier;

              while (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
              {
                  yield return null;
              }

              yield return new WaitForSeconds(1f);

              //escape the pod
              soundManager.Play(VOICE_ID_3) ;
              yield return new WaitForSeconds(12f);
              objectiveTracker.AddObjective(escapePodObjective, displaySpeed);
              safe.interactible = true;

            while(inventory.GetIngredientAmount(cannister) < 1 ||
                inventory.GetIngredientAmount(bundleOfWires) < 1 ||
                inventory.GetIngredientAmount(unstableFuelCell) < 1)
            {
                yield return new WaitForSeconds(0.5f);
            }

            objectiveTracker.AddObjective(craftExplosive, displaySpeed);

              while(inventory.GetIngredientAmount(explosiveDevice) < 1)
              {
                  yield return new WaitForSeconds(0.5f);
              }

              objectiveTracker.AddObjective(clearLandslideObjective, displaySpeed);
        }


        private IEnumerator DisplaySubObjectives()
        {
            ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();

            yield return new WaitForSeconds(0.5f);
            objectiveTracker.AddObjective(constructExplosiveSub1, displaySpeed);

            yield return new WaitForSeconds(0.5f);
            objectiveTracker.AddObjective(constructExplosiveSub2, displaySpeed);

            yield return new WaitForSeconds(0.5f);
            objectiveTracker.AddObjective(constructExplosiveSub3, displaySpeed);

            yield return new WaitForSeconds(0.2f);
            objectiveTracker.AddObjective(introObjectives[5], displaySpeed);
        }


        public void NextObjective(Objective objective, bool playMusic = false)
        {
            if(objective == constructExplosiveObjective)
            {
                autoIntensity.go = true;

                ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();
                ObjectiveDisplay od = objectiveTracker.AddObjective(objective, displaySpeed);
                if (playMusic) GameManager.Get<SoundManager>().Play(MUSIC_ID_1);
                GameManager.Get<SoundManager>().Play(SFX_ID_1);

                StartCoroutine(DisplaySubObjectives());
            }
            else
            {
                ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();
                ObjectiveDisplay od = objectiveTracker.AddObjective(objective, displaySpeed);
                if (playMusic) GameManager.Get<SoundManager>().Play(MUSIC_ID_1);
            }            
        }

        //TODO dont relly on this
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

