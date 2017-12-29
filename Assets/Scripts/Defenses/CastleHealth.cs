using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour, HealthInterface {

    public int vidaCastell;
    public int diners;
    private Text textVida;
    private Text textDiners;

    void Start()
    {
        textVida = GameObject.Find("Vida").GetComponent<Text>();
        textVida.text = vidaCastell.ToString();

        textDiners = GameObject.Find("Diners").GetComponent<Text>();
        textDiners.text = diners.ToString();
    }

    public int getVida()
    {
        return vidaCastell;
    }

    public void restarVida()
    {
        vidaCastell -= 1;
        textVida.text = (vidaCastell.ToString());
        if (vidaCastell == 0)
        {
          //  Destroy(gameObject);
        }
    }

    public bool restarDiners(int dinersARestar)
    {
        if (diners - dinersARestar > 0)
        {
            diners -= dinersARestar;
            textDiners.text = diners.ToString();
            return true;
        }
        else return false;
    }

    public void sumarDiners(int dinersASumar)
    {
        diners += dinersASumar;
        textDiners.text = diners.ToString();
    }
}
