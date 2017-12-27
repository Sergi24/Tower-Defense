using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovArrow : MonoBehaviour
{
    private GameObject[] objectius;
    private GameObject destination = null;
    private bool stopArrow = false;
    public float destroyTime;

    public float moveSpeed; //velocidad de movimiento 
    public float rotationSpeed; //Velocidad de rotación 

    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

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
}

    void Update()
    {
        if (!stopArrow)
        {
            //Rotacion para mirar hacia el target(objetivo a seguir) 
            if (destination != null)
            {
                Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.LookRotation(puntoDeChoque - myTransform.position), rotationSpeed * Time.deltaTime);
            }
            //Movimiento en dirección del target 
            myTransform.position += transform.forward * moveSpeed * Time.deltaTime;
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
            collision.gameObject.GetComponent<SoldierController>().restarVida();
        }
        stopArrow = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 0.2f);
    }

}
