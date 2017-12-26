using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int velocitatJugador=2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right*Time.deltaTime*velocitatJugador);
		if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left*Time.deltaTime*velocitatJugador);
		if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward*Time.deltaTime*velocitatJugador);
		if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.back*Time.deltaTime*velocitatJugador);
	}
}
