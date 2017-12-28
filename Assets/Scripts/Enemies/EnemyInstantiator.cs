using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour {

    public GameObject enemy;
    public int velocitatGeneracio;
    public int retardInicial;
    public int numRandom;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearEnemic", retardInicial, velocitatGeneracio);
    }

    void crearEnemic()
    {
        if (Random.Range(1, numRandom) == 1)
           {
              Instantiate(enemy, transform.position, transform.rotation);
           }
    }
}
