using UnityEngine;

public class WaveManager : MonoBehaviour {
    public GameObject soldier;
    public GameObject skeleton;
    public GameObject lich;
    public GameObject dragon;

    public GameObject spawnLocation;
    public CastleHealth castle;

    private int currentWave;
    private int enemyNumber;
    private int maxWaves;

    void Start () {
        castle = GameObject.Find("Player").GetComponent<CastleHealth>();
        currentWave = 0;
        enemyNumber = 0;
        maxWaves = 3;
	}

    void Update() {

        if(enemyNumber == 0  &&  currentWave<maxWaves ) {
            StartWave(currentWave++);
        }
        else if (castle.getVida() < 0) {
            Debug.Log("Game Over");
        }
        else if (enemyNumber <= 0  &&  currentWave==maxWaves) {
            Debug.Log("You win");
        }
        
    }

    public void notifyDeath() {
        Debug.Log(enemyNumber);
        enemyNumber--;
    } 
    



    System.Collections.IEnumerator SpawnEnemy( float delay, GameObject enemy, int number, GameObject location) {
        enemyNumber += number;
        yield return new WaitForSeconds(delay);
        for (int i=0; i< number; i++) {
            if (enemy == dragon) {
                Instantiate(enemy, location.transform.position + new Vector3(0, 5, 0), location.transform.rotation);
            }
            else Instantiate(enemy, location.transform.position, location.transform.rotation);
            yield return new WaitForSeconds(1);
        }
    }

    void StartWave (int waveNumber) {
        enemyNumber = 0;
        castle.reiniciarVida();

        RemoveDefenses();

        switch (waveNumber) {
            case 1:
                castle.setDiners(200);
                FirstWave();
                break;
            case 2:
                castle.setDiners(300);
                SecondWave();
                break;
            case 3:
                castle.setDiners(320);
                ThirdWave();
                break;
        }

    }

    void RemoveDefenses() {
        GameObject[] slotList = GameObject.FindGameObjectsWithTag("Slot");

        for (int i=0; i< slotList.Length; i++) {
            slotList[i].GetComponent<Builder>().RestartSlot();
        }
    }

    void FirstWave() {
        StartCoroutine(SpawnEnemy(0, soldier, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(5, skeleton, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(10, soldier, 20, spawnLocation));

        StartCoroutine(SpawnEnemy(30, skeleton, 10, spawnLocation));
        StartCoroutine(SpawnEnemy(35, skeleton, 5, spawnLocation));
        StartCoroutine(SpawnEnemy(35, soldier, 7, spawnLocation));
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
