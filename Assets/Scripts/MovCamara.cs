using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour {

	public int velocitatCamara = 6;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * velocitatCamara);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * velocitatCamara);
        if (Input.GetKey(KeyCode.Q) && transform.position.y > 12) transform.Translate(Vector3.down * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.E) && transform.position.y < 40) transform.Translate(Vector3.up * Time.deltaTime * velocitatCamara);
    }
}
