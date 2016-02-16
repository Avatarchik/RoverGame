using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraterExplosion : InteractibleObject
{
    public GameObject explosionPrefab1;
    public GameObject explosionPrefab2;
    public Transform explosionOrigin;
    public Ingredient desiredIngredient;
    public string failString = "You will need an {0} to clear this landslide";

    public float shakeIntensity = 1f;
    public float shakeDecay = 0.05f;

    public List<GameObject> rockWall = new List<GameObject>();

    private bool triggered = false;

    public override void Interact()
    {
        Inventory inventory = UIManager.GetMenu<Inventory>();
        MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();

        if(inventory.GetIngredientAmount(desiredIngredient) > 0)
        {
            triggered = true;
            inventory.RemoveInventoryItem(desiredIngredient, 1);
            StartCoroutine(DetonateDelay());
        }
        else if (!triggered)
        {
            StopAllCoroutines();
            messageMenu.Open(failString);
        }
    }

    public void TriggerExplosion()
    {
        CameraShake cameraShakeInstance = GameObject.FindObjectOfType<CameraShake>();
        GameObject explosion1 = Instantiate(explosionPrefab1, explosionOrigin.transform.position, explosionOrigin.transform.rotation) as GameObject;
        GameObject explosion2 = Instantiate(explosionPrefab2, explosionOrigin.transform.position, explosionOrigin.transform.rotation) as GameObject;
        explosion1.transform.SetParent(explosionOrigin);
        explosion2.transform.SetParent(explosionOrigin);

        cameraShakeInstance.Shake(shakeIntensity, shakeDecay);

        foreach(GameObject go in rockWall)
        {
            go.SetActive(false);
        }
    }


    private IEnumerator DetonateDelay()
    {
        yield return new WaitForSeconds(10f);
        TriggerExplosion();
    }


    private IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Close<MessageMenu>();
    }
}
