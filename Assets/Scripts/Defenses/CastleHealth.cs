using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour, HealthInterface {

    public int vidaCastell;
    private Text text;

    void Start()
    {
        text = GameObject.Find("Vida").GetComponent<Text>();
        text.text = "Vida: " + vidaCastell.ToString();
    }

    public int getVida()
    {
        return vidaCastell;
    }

    public void restarVida()
    {
        vidaCastell -= 1;
        text.text = (vidaCastell.ToString());
        if (vidaCastell == 0)
        {
          //  Destroy(gameObject);
        }
    }
}
