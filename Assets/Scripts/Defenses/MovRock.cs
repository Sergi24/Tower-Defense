using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRock : TorreBomba {

    private Rigidbody rb;
    private Vector3 zonaApuntada;
    private bool stopBomb = false;
    public float destroyTime;
    public float areaDany;
    public GameObject explosion1;
    public int damage;

    public float moveSpeed; //velocidad de movimiento 

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (!objectiuMesProper()) Destroy(gameObject);
        else
        {
            Invoke("DestruirBomba", destroyTime);
            rb.AddForce(Vector3.up * 50 * moveSpeed, ForceMode.Impulse);
            zonaApuntada = destination.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (!stopBomb)
        {
            //Movimiento en dirección del target 
           rb.AddForce(transform.forward * ((zonaApuntada - transform.position).magnitude) * moveSpeed);
        }
    }

    void DestruirBomba()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Atac" || collision.gameObject.tag != "Caballero")
        {
            stopBomb = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;

            //ENEMICS
            objectius = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < objectius.Length; i++)
            {
                if ((objectius[i].transform.position - transform.position).magnitude < areaDany)
                {
                    objectius[i].GetComponent<HealthInterface>().restarVida(damage);
                }
            }
            //DRAC
            objectius = GameObject.FindGameObjectsWithTag("Dragon");
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
}
