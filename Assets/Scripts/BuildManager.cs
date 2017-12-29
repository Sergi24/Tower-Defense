using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    private int defensePrice;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildMananger instanced");
        }
        instance = this;
    }

    public GameObject arrowTowerPrefab;
    public GameObject bombTowerPrefab;
    public GameObject catapultPrefab;

    private GameObject defenseToBuild;

    public GameObject GetDefenseToBuild() {
        return defenseToBuild;
    }

    public int GetDefensePrice()
    {
        return defensePrice;
    }

    public void SetDefenseToBuild(GameObject defense, int price) {
        defenseToBuild = defense;
        defensePrice = price;
    }

}

