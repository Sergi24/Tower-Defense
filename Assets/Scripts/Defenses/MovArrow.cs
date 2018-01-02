using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovArrow : MonoBehaviour
{
    private GameObject[] objectius;
    private GameObject destination = null;
    private GameObject destinationEnemic;
    private GameObject destinationDrac;
    private bool stopArrow = false;
    public float destroyTime;
    public int damage;
    Rigidbody rb;

    public float moveSpeed; //velocidad de movimiento 
    public float rotationSpeed; //Velocidad de rotación 

    private AudioSource asource;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float distanciaEnemic = funcioCerca("Enemy");
        if (destination != null) destinationEnemic = destination;
        float distanciaDrac = funcioCerca("Dragon");
        if (destination != null) destinationDrac = destination;

        if (distanciaDrac < distanciaEnemic)
        {
            destination = destinationDrac;
        } else destination = destinationEnemic;
        asource = gameObject.GetComponent<AudioSource>();

        Invoke("DestruirFletxa", destroyTime);
    }

    private float funcioCerca(string tag)
    {
        float distancia = 1000f;
        objectius = GameObject.FindGameObjectsWithTag(tag);

        //buscar objectiu mes proper
        float minim = 100f;
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((transform.position - objectius[i].transform.position).magnitude < minim)
            {
                minim = (transform.position - objectius[i].transform.position).magnitude;
                destination = objectius[i];
                distancia = (transform.position - objectius[i].transform.position).magnitude;
            }
        }
        
        return distancia;
    }

    void Update()
    {
        if (!stopArrow)
        {
            //Rotacion para mirar hacia el target(objetivo a seguir) 
            if (destination != null && destination.GetComponent<HealthInterface>().getVida()>0)
            {
                Vector3 puntoDeChoque;
                if (destination.tag == "Dragon") puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 4, destination.transform.position.z);
                else puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Dragon")
        {
            collision.gameObject.GetComponent<HealthInterface>().restarVida(damage);
            asource.Play();
        }
        stopArrow = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        rb.useGravity = true;
        Destroy(gameObject, 0.5f);
    }

}
