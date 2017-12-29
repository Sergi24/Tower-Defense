using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour {

	public int velocitatCamara = 6;
    public int limitBaix = 12;
    public int limitDalt = 40;
    public int limitDreta = 70;
    public int limitEsquerra = 4;
    public int limitEndavant = -47;
    public int limitEndarrera = -100;

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.W) && transform.position.z < limitEndavant) transform.Translate(Vector3.forward * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.S) && transform.position.z > limitEndarrera) transform.Translate(Vector3.back * Time.deltaTime * velocitatCamara);
        if (Input.GetKey(KeyCode.A) && transform.position.x > limitEsquerra) transform.Translate(Vector3.left * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.D) && transform.position.x < limitDreta) transform.Translate(Vector3.right * Time.deltaTime * velocitatCamara);
        if (Input.GetKey(KeyCode.Q) && transform.position.y > limitBaix) transform.Translate(Vector3.down * Time.deltaTime * velocitatCamara);
        else if (Input.GetKey(KeyCode.E) && transform.position.y < limitDalt) transform.Translate(Vector3.up * Time.deltaTime * velocitatCamara);
    }
}
