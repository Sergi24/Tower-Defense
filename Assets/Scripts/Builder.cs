using UnityEngine;

public class Builder : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    private Color initialColor;
    private Renderer rend;
    private GameObject defense;
    
	void Start () {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;

    }

    void OnMouseDown() {
        if (defense == null) {
            GameObject defenseToBuild = BuildManager.instance.GetDefenseToBuild();
            defense = (GameObject) Instantiate(defenseToBuild, transform.position + positionOffset, transform.rotation);
        }
    }

	
    void OnMouseEnter() {
        if (defense == null) {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit() {
        if (defense == null) {
            rend.material.color = initialColor;
        }
    }

}
