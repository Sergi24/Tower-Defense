using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour, HealthInterface {

    public int vidaTorre;
    public GameObject barraVida;
    private AudioSource asource;

    protected float vidaARestarDeBarra;

    private bool destroyed = false;

    void Start()
    {
        assignarVidaARestar();
        asource = gameObject.GetComponent<AudioSource>();
        asource.volume = 0.5f;
    }

    void Update()
    {
        if (destroyed)
        {
            transform.Translate(0f, -0.1f, 0f);
        }
    }

    public int getVida()
    {
        return vidaTorre;
    }

    public void restarVida(int vidaARestar)
    {
        vidaTorre -= vidaARestar;
        restarVidaBarra(vidaARestar);
        if (vidaTorre <= 0)
        {
            destroyed = true;
            asource.Play();
            Destroy(gameObject, 2f);
            barraVida.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    protected void assignarVidaARestar()
    {
        barraVida.GetComponent<MeshRenderer>().enabled = false;
        vidaARestarDeBarra = barraVida.transform.localScale.y / vidaTorre;
    }

    protected void restarVidaBarra(int vidaARestar)
    {
        if (vidaTorre > 0) barraVida.GetComponent<MeshRenderer>().enabled = true;
        if (barraVida!=null) barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x, barraVida.transform.localScale.y - (vidaARestarDeBarra * vidaARestar), barraVida.transform.localScale.z);
    }
}
