using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour, HealthInterface {

    public int vidaTorre;

    public int getVida()
    {
        return vidaTorre;
    }

    public void restarVida()
    {
        vidaTorre -= 1;
        if (vidaTorre == 0)
        {
            Destroy(gameObject);
        }
    }
}
