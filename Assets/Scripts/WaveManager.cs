using UnityEngine;

public class WaveManager : MonoBehaviour {
    public GameObject soldier;
    public GameObject skeleton;
    public GameObject lich;
    public GameObject dragon;
    public GameObject giantSoldier;

    public GameObject spawnLocation;
    public CastleHealth castle;
    public GameObject location2;

    public GameObject wavePanel;
    public TMPro.TextMeshProUGUI waveText;
    public GameObject pauseMenu;

    private int currentWave;
    private int enemyNumber;
    private int maxWaves;
    private int changeLocation = 0;

    void Start () {
        castle = GameObject.Find("Player").GetComponent<CastleHealth>();
        currentWave = 0;
        enemyNumber = 0;
        maxWaves = 3;
	}

    void Update() {
            if(enemyNumber == 0  &&  currentWave<=maxWaves ) {
                StartWave(currentWave++);
            }
            else if (castle.getVida() < 0 ) {
                FinalState("Game Over");
            }
            else if (enemyNumber <= 0  &&  currentWave==maxWaves+1) {
                FinalState("You win");
            }
    }

    public void notifyDeath() {
        enemyNumber--;
    } 



    System.Collections.IEnumerator SpawnEnemy( float delay, GameObject enemy, int number, GameObject location) {
        enemyNumber += number;
        yield return new WaitForSeconds(delay);
        for (int i=0; i< number; i++) {
            if (enemy == dragon)
            {
                Instantiate(enemy, location.transform.position + new Vector3(0, 5, 0), location.transform.rotation);
            } else if (enemy == giantSoldier)
            {
                Instantiate(enemy, location.transform.position, location.transform.rotation);
            }
            else
            {
                if (changeLocation>1)
                {
                    Instantiate(enemy, location2.transform.position, location.transform.rotation);
                    changeLocation=0;
                }
                else
                {
                    Instantiate(enemy, location.transform.position, location.transform.rotation);
                    changeLocation++;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void StartWave (int waveNumber) {
        enemyNumber = 0;

        ShowcaseWave(waveNumber);

        Time.timeScale = 0.5f; 

        switch (waveNumber) {
            case 1:
                castle.sumarDiners(100);
                FirstWave();
                break;
            case 2:
                castle.sumarDiners(200);
                SecondWave();
                break;
            case 3:
                castle.sumarDiners(300);
                ThirdWave();
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
    }

    void FinalState(string message) {
        Time.timeScale = 0f;
        wavePanel.SetActive(true);
        waveText.SetText(message);
        pauseMenu.SetActive(true);
    }


  void RemoveDefenses() {

        GameObject[] slotList = GameObject.FindGameObjectsWithTag("Slot");

        for (int i=0; i< slotList.Length; i++) {
            slotList[i].GetComponent<Builder>().RestartSlot();
        }
    }

    void FirstWave() {
        StartCoroutine(SpawnEnemy(0, skeleton, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(5, soldier, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(10, soldier, 20, spawnLocation));

        StartCoroutine(SpawnEnemy(50, skeleton, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(55, skeleton, 5, spawnLocation));
        StartCoroutine(SpawnEnemy(55, giantSoldier, 1, spawnLocation));
    }

    void SecondWave() {
        StartCoroutine(SpawnEnemy(0, skeleton, 8, spawnLocation));
        StartCoroutine(SpawnEnemy(3, soldier, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(3, lich, 4, spawnLocation));

        StartCoroutine(SpawnEnemy(20, soldier, 12, spawnLocation));
        StartCoroutine(SpawnEnemy(22, skeleton, 6, spawnLocation));
        StartCoroutine(SpawnEnemy(22, lich, 7, spawnLocation));

        StartCoroutine(SpawnEnemy(40, lich, 6, spawnLocation));
        StartCoroutine(SpawnEnemy(40, skeleton, 5, spawnLocation));
        StartCoroutine(SpawnEnemy(40, soldier, 8, spawnLocation));
    }

    void ThirdWave() {
        StartCoroutine(SpawnEnemy(0, dragon, 1, spawnLocation));
        StartCoroutine(SpawnEnemy(0, skeleton, 5, spawnLocation));
        StartCoroutine(SpawnEnemy(20, lich, 4, spawnLocation));
        StartCoroutine(SpawnEnemy(20, soldier, 5, spawnLocation));
        StartCoroutine(SpawnEnemy(30, skeleton, 15, spawnLocation));
    }

}
