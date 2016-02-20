using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Sol
{
    public class Harvesting : Menu
    {
        public ScannableTier tier1;
        public ScannableTier tier2;
        public ScannableTier tier3;

        public GameObject screenNotificationRoot;

        public Animator animator;
        public GameObject playerCamera;
        public float animationSpeed;
        public float pathResolution;
        public Transform lookPoint;

        public Transform[] cameraPath = new Transform[0];

        public override void Open()
        {
            Debug.Log("woot open");
            Travel(true);
            base.Open();
        }


        public void Open(InventoryIngredient i1, InventoryIngredient i2, InventoryIngredient i3, InventoryIngredient rareIngredient, float rareDropChance)
        {
            if (!isActive)
            {
                tier1.Initialize(i1, rareIngredient, rareDropChance);
                tier2.Initialize(i2, rareIngredient, rareDropChance);
                tier3.Initialize(i3, rareIngredient, rareDropChance);
                Open();
            }
        }


        public void OpenNote()
        {
            screenNotificationRoot.SetActive(true);
        }


        public void CloseNote()
        {

            screenNotificationRoot.SetActive(false);
        }


        public override void Close()
        {
            if (isActive)
            {
                root.SetActive(false);
                tier1.Halt();
                tier2.Halt();
                tier3.Halt();
                StartCoroutine(DelayedClose());
                Travel(false);
            }
        }


        private IEnumerator DelayedClose()
        {
            yield return new WaitForSeconds(2f);
            base.Close();
        }


        private void Travel(bool forwards)
        {
            animator.SetBool("Harvesting", forwards);
            Vector3[] path = new Vector3[cameraPath.Length];
            if (forwards)
            {
                for (int i = 0; i < cameraPath.Length; i++)
                {
                    path[i] = cameraPath[i].position;
                }
            }
            else
            {
                int count = 0;
                for (int i = cameraPath.Length - 1; i >= 0; i--)
                {
                    path[count] = cameraPath[i].position;
                    count++;
                }
            }

            float distance = Vector3.Distance(playerCamera.transform.position, path[0]);
            for (int i = 1; i < path.Length; i++)
            {
                distance += Vector3.Distance(path[i - 1], path[i]);
            }

            playerCamera.transform.DOPath(path, distance / animationSpeed, PathType.CatmullRom, PathMode.Full3D).SetLookAt(lookPoint);
        }


        private void LateUpdate()
        {
            playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, 0);
        }


        private void Awake()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        }
    }
}

