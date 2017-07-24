using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    Vector3 lastFramPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 currFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) //Righ or middle mouse button
        {
            Vector3 diff = lastFramPos - currFramePos;
            Camera.main.transform.Translate(diff);   
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");


        lastFramPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
