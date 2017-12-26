using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour {

	public int velocitatCamara = 6;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward * Time.deltaTime * velocitatCamara);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.back * Time.deltaTime * velocitatCamara);
    }
}
