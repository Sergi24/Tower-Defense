using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour {

    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearEnemic", 1, 1);
    }

    void crearEnemic()
    {
        if (Random.Range(1, 5) == 3)
           {
               Instantiate(enemy, transform.position, transform.rotation);
           }
    }
}
