using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovBall : MonoBehaviour
{
    private GameObject[] objectius;
    private GameObject destination = null;
    public GameObject explosion;
    private bool stopBola = false;
    public float destroyTime;

    public float moveSpeed; //velocidad de movimiento 
    private float velocitat;
    public int tempsEspera;
    public float rotationSpeed; //Velocidad de rotación 

    // Use this for initialization
    void Start()
    {
        objectius = GameObject.FindGameObjectsWithTag("Defensa");

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
        velocitat = 0;
        Invoke("fixarVelocitat", tempsEspera);
        Invoke("destruirBola", destroyTime);
    }

    void Update()
    {
        if (!stopBola)
        {
            //Rotacion para mirar hacia el target(objetivo a seguir) 
            if (destination != null)
            {
                Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);
            }
            //Movimiento en dirección del target 
            transform.position += transform.forward * velocitat * Time.deltaTime;
        }

    }

    void fixarVelocitat()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
        velocitat = moveSpeed;
    }

    void destruirBola()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Defensa")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.GetComponent<HealthInterface>().restarVida();
        }
        if (other.gameObject.tag != "Atac" && other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<HealthInterface>().restarVida();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
