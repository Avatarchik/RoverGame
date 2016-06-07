using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    [System.Serializable]
    public class CompletedObjectiveAttributes
    {
        public Color fontColor;
        public int fontSize;
    }


    public class ObjectiveTracker : Menu
    {
        public CompletedObjectiveAttributes completedObjectiveAttributes;

        public ObjectiveDisplay objectivePrefab;
        public PermanentObjectiveDisplay permanentObjectivePrefab;

        public Transform objectiveContainer;
        public Transform permanentObjectiveContainer;

        public Text objectiveTextAdmin;
        public Text objectiveTextAI;

        public string aiHexColor = "96C8FF";
        public string humanHexColor = "FFFFFF";

        public PersistentHUD persistentHud;

        public List<PermanentObjectiveDisplay> permanentlyDisplayedObjectives = new List<PermanentObjectiveDisplay>();
        public List<ObjectiveDisplay> displayedObjectives = new List<ObjectiveDisplay>();


        public override void Close()
        {
            if (IsActive)
            {
                if (closeEffect != null) CachedSoundManager.Play(closeEffect);

                CanvasGroup cg = root.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    StopCoroutine("FadeMenu");
                    StartCoroutine(FadeMenu(cg, 1f, 0f, true));
                }
                else
                {
                    isActive = false;
                    root.SetActive(false);
                }
            }
        }


        public override void Open()
        {
            if (!IsActive)
            {
                if (openEffect != null) CachedSoundManager.Play(openEffect);

                CanvasGroup cg = root.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    StopCoroutine("FadeMenu");
                    StartCoroutine(FadeMenu(cg, 0f, 1f));
                }

                isActive = true;
                root.SetActive(true);
            }
        }


        public void Open(string objective, bool admin = true, bool delayedClose = false, float delayedCloseTime = 4f)
        {
            if (admin)
            {
                objectiveTextAdmin.text = objective;
                objectiveTextAI.text = "";
                UIManager.GetMenu<LogMenu>().AddRadioLog("Jennifer > " + objective, humanHexColor);
            }
            else
            {
                objectiveTextAdmin.text = "";
                objectiveTextAI.text = objective;
                UIManager.GetMenu<LogMenu>().AddRadioLog("AI > " + objective, aiHexColor);
            }

            Open();

            StopCoroutine(DelayedClose(0));
            if (delayedClose) StartCoroutine(DelayedClose(delayedCloseTime));
        }


        public void ShowPermanentObjective(string objective)
        {
            if (!persistentHud.IsActive) persistentHud.Open();

            if(permanentlyDisplayedObjectives.Count > 0)
                StartCoroutine(FadeOldObjective(permanentlyDisplayedObjectives[permanentlyDisplayedObjectives.Count - 1]));

            PermanentObjectiveDisplay newPod = Instantiate(permanentObjectivePrefab);
            newPod.transform.SetParent(permanentObjectiveContainer);
            newPod.objectiveText.text = objective;

            permanentlyDisplayedObjectives.Add(newPod);
        }


        private IEnumerator FadeOldObjective(PermanentObjectiveDisplay pod)
        {
            float fadeTime = 1f;
            float elapsedTime = 0f;

            Color prevColor = pod.objectiveText.color;
            int prevSize = pod.objectiveText.fontSize;

            while(elapsedTime < fadeTime)
            {
                pod.objectiveText.color = Color.Lerp(prevColor, completedObjectiveAttributes.fontColor, elapsedTime / fadeTime);
                pod.objectiveText.fontSize = Mathf.RoundToInt(Mathf.Lerp(prevSize, completedObjectiveAttributes.fontSize, elapsedTime / fadeTime));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            pod.objectiveText.color = completedObjectiveAttributes.fontColor;
            pod.objectiveText.fontSize = completedObjectiveAttributes.fontSize;
            Debug.Log("done fading text");
        }


        private IEnumerator DelayedClose(float delayedCloseTime)
        {
            yield return new WaitForSeconds(delayedCloseTime);

            Close();
        }
    }

}