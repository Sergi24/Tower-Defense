using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour {

    public GameObject enemy;
    public int velocitatGeneracio;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearEnemic", velocitatGeneracio, velocitatGeneracio);
    }

    void crearEnemic()
    {
   //     if (Random.Range(1, 5) == 3)
    //       {
               Instantiate(enemy, transform.position, transform.rotation);
    //       }
    }
}
