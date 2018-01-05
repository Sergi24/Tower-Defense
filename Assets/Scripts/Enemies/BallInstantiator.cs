using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiator : MonoBehaviour {

    public GameObject ball;

    private GameObject[] objectius;

    public void crearBola()
    {
         Instantiate(ball, transform.position, transform.rotation);
    }
       
}
