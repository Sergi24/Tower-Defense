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
    private CastleHealth castleHealth;


    void Start()
    {
    buildManager = BuildManager.instance;
    buttonList = new Button[] { purchaseArrowButton, purchaseBombButton, purchaseKnightButton, purchaseCatapultButton };

    normalColorBlock = purchaseArrowButton.colors;

    pressedColorBlock = normalColorBlock;
    pressedColorBlock.normalColor = Color.white;
    pressedColorBlock.pressedColor = Color.red;

    castleHealth = GameObject.Find("Player").GetComponent<CastleHealth>();

    PurchaseArrowTower();
    }

    void Update()
    {
        if (castleHealth.getDiners() < 20) bloquejarTropa("TorreArquers", true);
        else bloquejarTropa("TorreArquers", false);

        if (castleHealth.getDiners() < 50) bloquejarTropa("TorreBomba", true);
        else bloquejarTropa("TorreBomba", false);

        if (castleHealth.getDiners() < 120) bloquejarTropa("Catapulta", true);
        else bloquejarTropa("Catapulta", false);

        if (castleHealth.getDiners() < 200) bloquejarTropa("Caballer", true);
        else bloquejarTropa("Caballer", false);
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

    void bloquejarTropa(string tropa, bool bloquejar)
    {
        Button buttonADesbloquejar;
        if (tropa.Equals("TorreArquers")) buttonADesbloquejar = purchaseArrowButton;
        else if (tropa.Equals("TorreBomba")) buttonADesbloquejar = purchaseBombButton;
        else if (tropa.Equals("Catapulta")) buttonADesbloquejar = purchaseCatapultButton;
        else buttonADesbloquejar = purchaseKnightButton;

        if (bloquejar) buttonADesbloquejar.GetComponent<Image>().color = Color.gray*0.5f;
        else buttonADesbloquejar.GetComponent<Image>().color = Color.white;
    }


}
