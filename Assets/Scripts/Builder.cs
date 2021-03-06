﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour {

    public Color hoverColor;
    public Color temporalColor;
    public Vector3 positionOffset;

    private Color initialColor;
    private Renderer rend;
    private GameObject defense;
    private BuildManager buildManager;
    private CastleHealth castleHealth;
    private bool apuntant = false;

    void Start () {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
        buildManager = BuildManager.instance;
        castleHealth = GameObject.Find("Player").GetComponent<CastleHealth>();
    }

    public void RestartSlot () {
        //Start();
        Destroy(defense);
        defense = null;
        rend.material.color = initialColor;
    }

    void Update()
    {
        if (defense == null && !apuntant)
        {
            rend.material.color = initialColor;
        }
    }

    void OnMouseDown() {
        if (buildManager.GetDefenseToBuild() == null) return;

        if ((defense == null) && (!IsBelowUI())) {
            if (castleHealth.restarDiners(buildManager.GetDefensePrice()))
            {
                GameObject defenseToBuild = BuildManager.instance.GetDefenseToBuild();
                defense = Instantiate(defenseToBuild, transform.position + positionOffset, transform.rotation);
                rend.material.color = hoverColor;
                apuntant = false;
            }
        }
    }

	
    void OnMouseEnter() {
        if ((defense == null) && (!IsBelowUI())) {
            apuntant = true;
            rend.material.color = temporalColor;
        }
    }

    void OnMouseExit() {
        if (defense == null) {
            rend.material.color = initialColor;
            apuntant = false;
        }
    }
    public bool IsBelowUI() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

}
