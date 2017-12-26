using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsInstantiator : MonoBehaviour {

    public GameObject fletxa;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearFletxa", 2, 2);
    }

    void crearFletxa()
    {
        Instantiate(fletxa, transform.position, transform.rotation);
    }
}
