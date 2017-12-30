using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreArquerosController : MonoBehaviour, HealthInterface {

    public int vidaTorre;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getVida()
    {
        return vidaTorre;
    }

    public void restarVida(int vidaARestar)
    {
        vidaTorre -= vidaARestar;
        if (vidaTorre < 0)
        {
            Destroy(gameObject);
        }
    }
}
