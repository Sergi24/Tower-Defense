using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovArrow : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject[] objectiu;
    private GameObject destination;

    public float destroyTime;
    private double lifeTime = 5;//segundos hasta eliminar flecha

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
        rb = GetComponent<Rigidbody>();
        objectiu = GameObject.FindGameObjectsWithTag("Enemy");

        if (objectiu.Length > 0) {
            //buscar objectiu mes proper
            Vector3 minim = new Vector3(100f, 100f, 100f);
            destination = objectiu[0];
            for (int i = 0; i < objectiu.Length; i++)
            {
                if ((transform.position - objectiu[i].transform.position).magnitude < minim.magnitude)
                {
                    minim = transform.position - objectiu[i].transform.position;
                    destination = objectiu[i];
                }
            }
        }

        Invoke("DestruirFletxa", destroyTime);
        Debug.Log(Time.time, this);
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
        if (destination != null)
        {
            Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, destination.transform.position.y + 1, destination.transform.position.z);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
            Quaternion.LookRotation(puntoDeChoque - myTransform.position), rotationSpeed * Time.deltaTime);

            //Movimiento en dirección del target 
            myTransform.position += transform.forward * moveSpeed * Time.deltaTime;
        }else
        {
            myTransform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
  
}

    void DestruirFletxa()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

}
