using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInstantiator : MonoBehaviour {

    public GameObject fire;

    public void instantiateFire()
    {
        Instantiate(fire, transform.position, transform.rotation);
    }
}
