using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroInstantiator : MonoBehaviour {

    public GameObject caballero;

    public void crearCaballero()
    {
        Instantiate(caballero, transform.position, transform.rotation);
    }
}
