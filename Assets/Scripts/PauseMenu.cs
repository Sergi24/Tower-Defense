using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuPanel;

    public GameObject textVelocitat;
    private bool velocitatAumentada = false;

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

    public void AumentarVelocitat()
    {
        if (velocitatAumentada)
        {
            Time.timeScale = 1f;
            velocitatAumentada = false;
            textVelocitat.GetComponent<TMPro.TextMeshProUGUI>().SetText("x1");
        }
        else
        {
            Time.timeScale = 2f;
            velocitatAumentada = true;
            textVelocitat.GetComponent<TMPro.TextMeshProUGUI>().SetText("x2");
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
