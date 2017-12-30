using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour, HealthInterface {

    public int vidaCastell;
    public int diners;

    public Slider barraVida;
    private TMPro.TextMeshProUGUI textVida;
    private TMPro.TextMeshProUGUI textDiners;

    void Start()
    {
        barraVida.value = vidaCastell;

        textVida = GameObject.Find("Vida").GetComponent<TMPro.TextMeshProUGUI>();
        textVida.text = vidaCastell.ToString();

        textDiners = GameObject.Find("Diners").GetComponent<TMPro.TextMeshProUGUI>();
        textDiners.text = diners.ToString();
    }

    public int getVida()
    {
        return vidaCastell;
    }

    public void reiniciarVida() {
        vidaCastell = 100;
    }

    public void restarVida(int vidaARestar)
    {

        if (vidaCastell - vidaARestar > 0)
        {
            vidaCastell -= vidaARestar;
            textVida.text = vidaCastell.ToString();
            barraVida.value = vidaCastell;
            //  Destroy(gameObject);
        } else
        {
            //PERDUT
            textVida.text = 0.ToString();
            barraVida.value = 0;
        }
    }

    public bool restarDiners(int dinersARestar)
    {
        if (diners - dinersARestar >= 0)
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

    public void setDiners(int valor) {
        diners = valor;
        textDiners.text = diners.ToString();
    }
}
