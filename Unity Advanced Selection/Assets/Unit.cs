using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool selected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Renderer>().isVisible && Input.GetMouseButton(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = Selection.ScreenToRectSpaceY(camPos.y);
            selected = Selection.selection.Contains(camPos);
        }

        if(selected)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
	
	}
}
