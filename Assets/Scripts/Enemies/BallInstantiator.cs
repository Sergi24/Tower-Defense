using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiator : MonoBehaviour {

    public GameObject ball;
    public float velocitatDisparo;
    public int rangAvast;

    private GameObject[] objectius;

    // Use this for initialization
    void Start()
    {
      //  InvokeRepeating("crearBola", velocitatDisparo, velocitatDisparo);
    }

    public void crearBola()
    {
         Instantiate(ball, transform.position, transform.rotation);
    }
       
}
