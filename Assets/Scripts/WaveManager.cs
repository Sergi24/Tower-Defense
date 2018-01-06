using UnityEngine;

public class WaveManager : MonoBehaviour {
    public GameObject soldier;
    public GameObject skeleton;
    public GameObject lich;
    public GameObject dragon;
    public GameObject giantSoldier;

    public GameObject location1;
    public CastleHealth castle;
    public GameObject location2;

    public GameObject wavePanel;
    public TMPro.TextMeshProUGUI waveText;
    public TMPro.TextMeshProUGUI waveText2;
    public GameObject pauseMenu;
    public GameObject canvas;
    private int relativeDelay1;
    private int relativeDelay2;

    private int currentWave;
    private int enemyNumber;
    private int maxWaves;

    void Start () {
        castle = GameObject.Find("Player").GetComponent<CastleHealth>();
        currentWave = 0;
        enemyNumber = 0;
        relativeDelay1 = 0;
        relativeDelay2 = 0;
        maxWaves = 4;
	}

    void Update() {
            if(enemyNumber == 0  &&  currentWave<=maxWaves ) {
                StartWave(currentWave++);
            }
            if (castle.getVida() <= 0 ) {
                FinalState("Game Over");
            }
            if (enemyNumber <= 0  &&  currentWave==maxWaves+1) {
                FinalState("You win");
            }
    }

    public void notifyDeath() {
        enemyNumber--;
    } 



    System.Collections.IEnumerator SpawnEnemy1(int delay, GameObject enemy, int number, bool horde) {
        relativeDelay1 += delay;
        enemyNumber += number;
        yield return new WaitForSeconds(relativeDelay1);
        for (int i=0; i< number; i++) {
            if (enemy == dragon)
            {
                Instantiate(enemy, location1.transform.position + new Vector3(0, 5, 0), location1.transform.rotation);
            } else
            { 
                Instantiate(enemy, location1.transform.position, location1.transform.rotation);
            }
            if (!horde) yield return new WaitForSeconds(1f);
        }
    }

    System.Collections.IEnumerator SpawnEnemy2(int delay, GameObject enemy, int number, bool horde)
    {
        relativeDelay2 += delay;
        enemyNumber += number;
        yield return new WaitForSeconds(relativeDelay2);
        for (int i = 0; i < number; i++)
        {
            if (enemy == dragon)
            {
                Instantiate(enemy, location2.transform.position + new Vector3(0, 5, 0), location2.transform.rotation);
            }
            else
            {
                Instantiate(enemy, location2.transform.position, location2.transform.rotation);
            }
            if (!horde) yield return new WaitForSeconds(1f);
        }
    }

    void StartWave (int waveNumber) {

        enemyNumber = 0;

        ShowcaseWave(waveNumber);

        Time.timeScale = 0.5f;

        relativeDelay1 = 0;
        relativeDelay2 = 0;

        switch (waveNumber) {
            case 1:
                FirstWave();
                break;
            case 2:
                castle.sumarDiners(250);
                waveText2.SetText("Extra gold: " + 250);
                SecondWave();
                break;
            case 3:
                castle.sumarDiners(300);
                waveText2.SetText("Extra gold: " + 300);
                ThirdWave();
                break;
            case 4:
                castle.sumarDiners(350);
                waveText2.SetText("Extra gold: " + 350);
                FourthWave();
                break;
        }

        canvas.GetComponent<PauseMenu>().SetVelocitatActual(1f);

    }

    void ShowcaseWave(int waveNumber) {
        wavePanel.SetActive(true);
        waveText.SetText("Wave " + waveNumber);
        Invoke("HideWaveText", 4f);

    }

    void HideWaveText() {
        wavePanel.SetActive(false);
        if (maxWaves == currentWave) waveText2.SetText("");
    }

    void FinalState(string message) {
        wavePanel.SetActive(true);
        waveText.SetText(message);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }


  void RemoveDefenses() {

        GameObject[] slotList = GameObject.FindGameObjectsWithTag("Slot");

        for (int i=0; i< slotList.Length; i++) {
            slotList[i].GetComponent<Builder>().RestartSlot();
        }
    }

    void FirstWave() {
        StartCoroutine(SpawnEnemy1(5, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(10, skeleton, 5, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 5, false));
        StartCoroutine(SpawnEnemy1(10, soldier, 5, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 20, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(30, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(1, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(1, soldier, 3, true));
        StartCoroutine(SpawnEnemy1(0, lich, 1, true));
        StartCoroutine(SpawnEnemy1(1, soldier, 3, true));

        StartCoroutine(SpawnEnemy2(25, soldier, 3, false));
        StartCoroutine(SpawnEnemy2(5, skeleton, 10, false));
        StartCoroutine(SpawnEnemy2(10, skeleton, 10, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 4, true));
        StartCoroutine(SpawnEnemy2(2, lich, 1, true));
    }

    void SecondWave() {
        StartCoroutine(SpawnEnemy1(0, skeleton, 30, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 30, false));
        StartCoroutine(SpawnEnemy1(5, soldier, 10, false));
        StartCoroutine(SpawnEnemy1(10, soldier, 3, true));
        StartCoroutine(SpawnEnemy1(3, lich, 1, true));
        StartCoroutine(SpawnEnemy1(20, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 6, false));
        StartCoroutine(SpawnEnemy1(30, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(50, dragon, 1, true));
        StartCoroutine(SpawnEnemy1(4, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(4, skeleton, 40, false));

        StartCoroutine(SpawnEnemy2(30, soldier, 8, true));
        StartCoroutine(SpawnEnemy2(10, skeleton, 15, true));
        StartCoroutine(SpawnEnemy1(5, lich, 1, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 8, true));
        StartCoroutine(SpawnEnemy2(10, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(30, giantSoldier, 1, true));
        StartCoroutine(SpawnEnemy2(30, soldier, 5, true));
    }

    void ThirdWave() {
        StartCoroutine(SpawnEnemy1(0, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 50, false));
        StartCoroutine(SpawnEnemy1(4, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(1, giantSoldier, 1, true));
        StartCoroutine(SpawnEnemy1(1, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(30, soldier, 10, true));
        StartCoroutine(SpawnEnemy1(10, soldier, 3, true));
        StartCoroutine(SpawnEnemy1(3, lich, 2, true));
        StartCoroutine(SpawnEnemy1(40, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 6, false));
        StartCoroutine(SpawnEnemy1(30, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(20, dragon, 1, true));
        StartCoroutine(SpawnEnemy1(4, skeleton, 40, false));

        StartCoroutine(SpawnEnemy2(5, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(30, soldier, 8, true));
        StartCoroutine(SpawnEnemy2(5, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 8, true));
        StartCoroutine(SpawnEnemy2(3, lich, 1, true));
        StartCoroutine(SpawnEnemy2(10, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(15, skeleton, 40, false));
        StartCoroutine(SpawnEnemy2(5, skeleton, 40, false));
        StartCoroutine(SpawnEnemy2(0, dragon, 1, true));
    }

    void FourthWave()
    {
        StartCoroutine(SpawnEnemy1(0, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(5, giantSoldier, 2, true));
        StartCoroutine(SpawnEnemy1(30, soldier, 10, true));
        StartCoroutine(SpawnEnemy1(10, soldier, 3, true));
        StartCoroutine(SpawnEnemy1(0, dragon, 1, true));
        StartCoroutine(SpawnEnemy1(3, lich, 4, true));
        StartCoroutine(SpawnEnemy1(40, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 6, false));
        StartCoroutine(SpawnEnemy1(30, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(20, dragon, 1, true));
        StartCoroutine(SpawnEnemy1(0, giantSoldier, 2, true));
        StartCoroutine(SpawnEnemy1(2, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(4, skeleton, 40, false));
        StartCoroutine(SpawnEnemy1(4, lich, 30, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 30, false));
        StartCoroutine(SpawnEnemy1(20, dragon, 1, true));
        StartCoroutine(SpawnEnemy1(0, skeleton, 30, false));

        StartCoroutine(SpawnEnemy2(5, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(30, soldier, 8, true));
        StartCoroutine(SpawnEnemy2(5, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 10, true));
        StartCoroutine(SpawnEnemy2(0, soldier, 10, true));
        StartCoroutine(SpawnEnemy2(0, soldier, 10, true));
        StartCoroutine(SpawnEnemy2(15, lich, 4, true));
        StartCoroutine(SpawnEnemy2(30, giantSoldier, 1, true));
        StartCoroutine(SpawnEnemy2(10, skeleton, 15, true));
        StartCoroutine(SpawnEnemy2(15, skeleton, 40, false));
        StartCoroutine(SpawnEnemy2(5, skeleton, 40, false));
        StartCoroutine(SpawnEnemy2(15, skeleton, 40, false));
        StartCoroutine(SpawnEnemy2(20, dragon, 1, true));
        StartCoroutine(SpawnEnemy2(40, giantSoldier, 2, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 4, true));
        StartCoroutine(SpawnEnemy2(3, lich, 10, true));
        StartCoroutine(SpawnEnemy2(1, soldier, 4, true));
    }

}
