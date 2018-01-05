using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovBall : TroopGeneralControl
{
    public GameObject explosion;
    private bool stopBola = false;
    public float destroyTime;
    private float velocitat;

    public int tempsEspera;

    // Use this for initialization
    void Start()
    {
        if (!findClosestTarget("Player", rangAtac) || transform.position.z > -73)
          if (!findClosestTarget("Caballero", rangAtac))
            if (!findClosestTarget("Defensa", rangAtac)) destination = GameObject.Find("Player");
        velocitat = 0;
        Invoke("fixarvelocitat", tempsEspera);
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

    void fixarvelocitat()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
        velocitat = velocitatMoviment;
    }

    void destruirBola()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Defensa" || other.gameObject.tag == "Player" || other.gameObject.tag == "Caballero")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.GetComponent<HealthInterface>().restarVida(damage);
            Destroy(gameObject);
        }
    //    if (other.gameObject.tag != "Atac" && other.gameObject.tag != "Enemy")
   //     {
    //        Destroy(gameObject);
   //     }
    }
}
