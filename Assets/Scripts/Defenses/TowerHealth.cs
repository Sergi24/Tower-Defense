using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour, HealthInterface {

    public int vidaTorre = 0;

    public int getVida()
    {
        return vidaTorre;
    }

    public void restarVida(int vidaARestar)
    {
        vidaTorre -= vidaARestar;
        if (vidaTorre <= 0)
        {
            Destroy(gameObject);
        }
    }
}
