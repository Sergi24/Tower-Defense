using UnityEngine;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    public UnityEngine.UI.Button archerTowerButton;
    public UnityEngine.UI.Button bombTowerButton;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseArrowTower () {
        buildManager.SetDefenseToBuild(buildManager.arrowTowerPrefab);
        archerTowerButton.enabled = true;
        Debug.Log("Arrow tower purchased");
    }

    public void PurchaseBombTower() {
        bombTowerButton.enabled = true;
        buildManager.SetDefenseToBuild(buildManager.bombTowerPrefab);
        Debug.Log("Bomb tower purchased");
    }


}
