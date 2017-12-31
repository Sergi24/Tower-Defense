using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBomb : MonoBehaviour {

    
    private GameObject[] objectius;
    private GameObject destination = null;
    private bool stopBomb = false;
    public float destroyTime;
    public float areaDany;
    public GameObject explosion1;
    public int damage;

    public float moveSpeed; //velocidad de movimiento 
    public float rotationSpeed; //Velocidad de rotación 

    // Use this for initialization
    void Start()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");

        //buscar objectiu mes proper
        float minim = 10000f;
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((transform.position - objectius[i].transform.position).magnitude < minim)
            {
                if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                { 
                    minim = (transform.position - objectius[i].transform.position).magnitude;
                    destination = objectius[i];
                }
            }
        }
        //Rotacion para mirar hacia el target(objetivo a seguir) 
        if (destination != null)
        {
            Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y+2, destination.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntoDeChoque - transform.position), 1);
        }
        Invoke("DestruirBomba", destroyTime);
    }

    void Update()
    {
        if (!stopBomb)
        {
            //Movimiento en dirección del target 
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void DestruirBomba()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        stopBomb = true;
        gameObject.GetComponent<SphereCollider>().enabled = false;

        objectius = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((objectius[i].transform.position - transform.position).magnitude < areaDany)
            {
                objectius[i].GetComponent<HealthInterface>().restarVida(damage);
            }
        }
        Instantiate(explosion1, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
