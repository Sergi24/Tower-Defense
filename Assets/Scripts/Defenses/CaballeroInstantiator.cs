using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroInstantiator : MonoBehaviour {

    public GameObject caballero;
    private int contador = 0;
    public int velocitatAlAguantar;

    public void crearCaballero()
    {
        Instantiate(caballero, transform.position, transform.rotation);
    }
}
