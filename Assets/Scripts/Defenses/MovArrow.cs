using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovArrow : MonoBehaviour
{
    private GameObject[] objectius;
    private GameObject destination = null;
    private bool stopArrow = false;
    public float destroyTime;
    Rigidbody rb;

    public float moveSpeed; //velocidad de movimiento 
    public float rotationSpeed; //Velocidad de rotación 

    // Use this for initialization
    void Start()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");

        //buscar objectiu mes proper
        float minim = 100f;
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((transform.position - objectius[i].transform.position).magnitude < minim)
            {
                minim = (transform.position - objectius[i].transform.position).magnitude;
                destination = objectius[i];
            }
        }
        Invoke("DestruirFletxa", destroyTime);
        rb = GetComponent<Rigidbody>();
}

    void Update()
    {
        if (!stopArrow)
        {
            //Rotacion para mirar hacia el target(objetivo a seguir) 
            if (destination != null)
            {
                Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);
            }
            //Movimiento en dirección del target 
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void DestruirFletxa()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthInterface>().restarVida();
        }
        stopArrow = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        rb.useGravity = true;
        Destroy(gameObject, 0.2f);
    }

}
