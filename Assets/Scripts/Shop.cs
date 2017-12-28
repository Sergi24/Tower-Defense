using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseArrowTower () {
        buildManager.SetDefenseToBuild(buildManager.arrowTowerPrefab);
        Debug.Log("Arrow tower purchased");
    }

    public void PurchaseBombTower() {
        buildManager.SetDefenseToBuild(buildManager.bombTowerPrefab);
        Debug.Log("Bomb tower purchased");
    }

    public void PurchaseCatapult() {
        buildManager.SetDefenseToBuild(buildManager.catapultPrefab);
        Debug.Log("Catapult purchased");
    }

    public void PurchaseKnight() {
        GameObject[] list = GameObject.FindGameObjectsWithTag("CaballeroInstantiator");
        for (int i = 0; i < list.Length; i++) {
            list[i].GetComponent<CaballeroInstantiator>().crearCaballero();
        }
        Debug.Log("Knight purchased");
    }


}
