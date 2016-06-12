using UnityEngine;
using System.Collections;

public class RandomRange : MonoBehaviour {

    //Animator animator;
    int randomnumber01 = 0;
    public Animator WindTurbine01;
    // Use this for initialization
    void Start () {

        WindTurbine01 = GetComponent<Animator>();
        
}
	
	// Update is called once per frame
	void Update () {
        randomnumber01 = Random.Range(1, 4);

        WindTurbine01.SetInteger("randomnumber01", randomnumber01);
    }
}
