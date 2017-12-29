using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

BuildManager buildManager;
public Button purchaseArrowButton;
public Button purchaseBombButton;
public Button purchaseKnightButton;
public Button purchaseCatapultButton;

private ColorBlock normalColorBlock;
private ColorBlock pressedColorBlock;
private Button[] buttonList;


void Start()
{
    buildManager = BuildManager.instance;
    buttonList = new Button[] { purchaseArrowButton, purchaseBombButton, purchaseKnightButton, purchaseCatapultButton };

    normalColorBlock = purchaseArrowButton.colors;

    pressedColorBlock = normalColorBlock;
    pressedColorBlock.normalColor = Color.white;
    pressedColorBlock.pressedColor = Color.gray;
}

public void PurchaseArrowTower()
{
    buildManager.SetDefenseToBuild(buildManager.arrowTowerPrefab, 20);
    SelectButton("BuildArcherTower");
}

public void PurchaseBombTower()
{
    buildManager.SetDefenseToBuild(buildManager.bombTowerPrefab, 50);
    SelectButton("BuildBombTower");
}

public void PurchaseCatapult()
{
    buildManager.SetDefenseToBuild(buildManager.catapultPrefab, 120);
    SelectButton("BuildCatapult");
}

public void PurchaseKnight()
{
    if (GameObject.Find("Player").GetComponent<CastleHealth>().restarDiners(200))
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("CaballeroInstantiator");
        for (int i = 0; i < list.Length; i++)
        {
            list[i].GetComponent<CaballeroInstantiator>().crearCaballero();
        }
        SelectButton("clean pressed buttons");
    }
}

void SelectButton(string buttonName)
{
    foreach (Button b in buttonList)
    {
        if (b.name == buttonName)
        {
            b.colors = pressedColorBlock;
        }
        else b.colors = normalColorBlock;
    }
}


}
