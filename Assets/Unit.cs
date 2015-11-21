using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool selected{get;set;}
    public Color materialColor{get;set;}

	// Use this for initialization
	void Start () {
        materialColor = GetComponent<Renderer>().material.color;	
	}
}
