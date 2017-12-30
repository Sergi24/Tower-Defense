using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuPanel;

    void Start() {
        pauseMenuPanel.SetActive(false);
    }

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Escape");
            if(GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
	}

    void Pause() {
        pauseMenuPanel.SetActive(true);
        Time.timeScale= 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Quit() {
        Application.Quit();
    }


}
