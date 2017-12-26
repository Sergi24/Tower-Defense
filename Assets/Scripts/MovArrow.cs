using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovArrow : MonoBehaviour
{

    public float velocitatFletxa = 10f;
    private Rigidbody rb;
    private GameObject[] objectiu;
    private GameObject destination;

    private float moveSpeed = 3; //velocidad de movimiento 
    private float rotationSpeed = 3f; //Velocidad de rotación 

    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectiu = GameObject.FindGameObjectsWithTag("Player");

        //buscar objectiu mes proper
        Vector3 maxim = new Vector3(10f, 10f, 10f);
        destination = objectiu[0];
        for (int i = 0; i < objectiu.Length; i++)
        {
            if ((transform.position - objectiu[i].transform.position).magnitude > maxim.magnitude)
            {
                maxim = transform.position - objectiu[i].transform.position;
                destination = objectiu[i];
            }
        }
    }

    // Update is called once per frame
    /*  void FixedUpdate()
      {
          //  transform.Translate(new Vector3(0, 0, -velocitatFletxa * Time.deltaTime));
          rb.AddForce(0, 0, velocitatFletxa);
          transform.Rotate(1f, 0, 0);
      }
      */
    void Update()
    {
        //Rotacion para mirar hacia el target(objetivo a seguir) 
        Quaternion.Euler(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z);
        myTransform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z),
        Quaternion.LookRotation(destination.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //Movimiento en dirección del target 
        myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;
    }

}
