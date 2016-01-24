using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour {

	Material sky;

	public Transform stars;


    void Start () 
	{

		sky = RenderSettings.skybox;

	}


    void FixedUpdate () 
	{

		stars.transform.rotation = transform.rotation;
	}
}
