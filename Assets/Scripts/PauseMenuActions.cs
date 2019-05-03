using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActions : MonoBehaviour {
    public GameObject optionsMenu;
    public GameObject controlsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause() {
        gameObject.SetActive(true);
        GameManager.instance.isStopped = true;
        Time.timeScale = 0.0f;
    }

    public void Resume() {
        gameObject.SetActive(false);
        GameManager.instance.isStopped = false;
        Time.timeScale = 1.0f;
    }

    public void OpenOptions() {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenControls() {
        controlsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RestartLevel() {
        Time.timeScale = 1.0f;
        GameManager.instance.GetComponent<LevelManagement>().ResetLevel();
    }

    public void ExitMainMenu() {
        Time.timeScale = 1.0f;
        GameManager.instance.GetComponent<LevelManagement>().ExitMainMenu();
    }
}
