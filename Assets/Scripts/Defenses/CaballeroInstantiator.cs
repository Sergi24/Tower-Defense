using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroInstantiator : MonoBehaviour {

    public GameObject caballero;
    private int contador = 0;
    public int velocitatAlAguantar;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (contador > velocitatAlAguantar)
            {
                crearCaballero();
                contador = 0;
            }
            else contador++;
        }
    }

    public void crearCaballero()
    {
        Instantiate(caballero, transform.position, transform.rotation);
    }
}
