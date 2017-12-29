using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public int destroyTime;
	// Use this for initialization
	void Start () {
        Invoke("destroy", destroyTime);
	}
	
	// Update is called once per frame
	void destroy () {
        Destroy(gameObject);
	}
}
