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
    public int relativeDelay;
    public int relativeDelay2;

    private int currentWave;
    private int enemyNumber;
    private int maxWaves;

    void Start () {
        castle = GameObject.Find("Player").GetComponent<CastleHealth>();
        currentWave = 0;
        enemyNumber = 0;
        relativeDelay = 0;
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
        relativeDelay += delay;
        enemyNumber += number;
        yield return new WaitForSeconds(relativeDelay);
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

        switch (waveNumber) {
            case 1:
                FirstWave();
                break;
            case 2:
                castle.sumarDiners(200);
                waveText2.SetText("Extra gold: " + 200);
                SecondWave();
                break;
            case 3:
                castle.sumarDiners(300);
                waveText2.SetText("Extra gold: " + 300);
                ThirdWave();
                break;
            case 4:
                castle.sumarDiners(400);
                waveText2.SetText("Extra gold: " + 400);
                FourthWave();
                break;
        }

        Time.timeScale = 1f;

    }

    void ShowcaseWave(int waveNumber) {
        wavePanel.SetActive(true);
        waveText.SetText("Wave " + waveNumber);
        Invoke("HideWaveText", 1.3f);

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
        StartCoroutine(SpawnEnemy1(0, skeleton, 20, false));
        StartCoroutine(SpawnEnemy1(11, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(10, soldier, 5, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 20, false));
        StartCoroutine(SpawnEnemy1(5, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(30, skeleton, 10, true));
        StartCoroutine(SpawnEnemy1(1, giantSoldier, 1, true));
        StartCoroutine(SpawnEnemy1(1, skeleton, 10, true));

        StartCoroutine(SpawnEnemy2(20, soldier, 3, false));
        StartCoroutine(SpawnEnemy2(5, skeleton, 10, false));
        StartCoroutine(SpawnEnemy2(10, skeleton, 10, true));
        StartCoroutine(SpawnEnemy2(5, soldier, 2, true));
        StartCoroutine(SpawnEnemy2(2, lich, 1, true));
    }

    void SecondWave() {
        StartCoroutine(SpawnEnemy1(0, skeleton, 8, false));
        StartCoroutine(SpawnEnemy1(1, skeleton, 10, false));
        StartCoroutine(SpawnEnemy1(3, soldier, 10, false));
        StartCoroutine(SpawnEnemy1(3, lich, 4, false));

        StartCoroutine(SpawnEnemy1(20, soldier, 12, false));
        StartCoroutine(SpawnEnemy1(22, skeleton, 6, false));
        StartCoroutine(SpawnEnemy1(50, lich, 3, false));

        StartCoroutine(SpawnEnemy2(40, soldier, 6, false));
        StartCoroutine(SpawnEnemy2(80, lich, 4, false));
        StartCoroutine(SpawnEnemy2(40, soldier, 8, false));
    }

    void ThirdWave() {
        StartCoroutine(SpawnEnemy1(0, dragon, 1, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 5, false));
        StartCoroutine(SpawnEnemy1(20, lich, 4, false));
        StartCoroutine(SpawnEnemy1(20, soldier, 5, false));
        StartCoroutine(SpawnEnemy1(30, skeleton, 15, false));
    }

    void FourthWave()
    {
        StartCoroutine(SpawnEnemy1(0, dragon, 1, false));
        StartCoroutine(SpawnEnemy1(0, skeleton, 5, false));
        StartCoroutine(SpawnEnemy1(20, lich, 4, false));
        StartCoroutine(SpawnEnemy1(20, soldier, 5, false));
        StartCoroutine(SpawnEnemy1(30, skeleton, 15, false));
    }

}
