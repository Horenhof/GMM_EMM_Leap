using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : MonoBehaviour {


    private GameObject welcome;
	// Use this for initialization
	void Start () {
        welcome = GameObject.FindGameObjectWithTag("welcomeFahrer");
        Debug.Log(welcome);
        Shader s = welcome.GetComponent<Shader>();
        Debug.Log(s);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
