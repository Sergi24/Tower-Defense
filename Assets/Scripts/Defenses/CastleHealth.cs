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
    public GameObject drippingFlames1;
    public GameObject drippingFlames2;
    public GameObject drippingFlames3;
    public GameObject drippingFlames4;

    public GameObject wavePanel;
    public TMPro.TextMeshProUGUI waveText;


    void Start()
    {
        barraVida.value = vidaCastell;

        textVida = GameObject.Find("Vida").GetComponent<TMPro.TextMeshProUGUI>();
        textVida.text = vidaCastell.ToString();

        textDiners = GameObject.Find("Diners").GetComponent<TMPro.TextMeshProUGUI>();
        textDiners.text = diners.ToString();
    }

    void Update()
    {
        if (vidaCastell < 60)
        {
            drippingFlames1.SetActive(true);
        }
        if (vidaCastell < 45)
        {
            drippingFlames2.SetActive(true);
        }
        if (vidaCastell < 25)
        {
            drippingFlames3.SetActive(true);
        }
        if (vidaCastell < 10)
        {
            drippingFlames4.SetActive(true);
        }
        if (vidaCastell <= 0)
        {
       //     partidaPerduda();
        }
    }

    public int getVida()
    {
        return vidaCastell;
    }

    void partidaPerduda()
    {
        wavePanel.SetActive(true);
        waveText.SetText("You lose");
    //    Invoke("HideWaveText", 1.3f);
    }

    void HideWaveText()
    {
        wavePanel.SetActive(false);
    }

    public void reiniciarVida() {
        vidaCastell = 100;
    }

    public void restarVida(int vidaARestar)
    {
        vidaCastell -= vidaARestar;
        if (vidaCastell - vidaARestar > 0)
        {
            textVida.text = vidaCastell.ToString();
            barraVida.value = vidaCastell;
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
