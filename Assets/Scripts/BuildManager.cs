using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildMananger instanced");
        }
        instance = this;
    }

    public GameObject arrowTowerPrefab;
    public GameObject bombTowerPrefab;

    private GameObject defenseToBuild;

    public GameObject GetDefenseToBuild() {
        return defenseToBuild;
    }

    public void SetDefenseToBuild(GameObject defense) {
        defenseToBuild = defense;
    }
}

