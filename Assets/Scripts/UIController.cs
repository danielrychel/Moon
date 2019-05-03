using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause")) {
            if(gameOverMenu.activeSelf) {
                // ignore
            }
            else if(controlsMenu.activeSelf) {
                controlsMenu.GetComponent<ControlsMenuActions>().Back();
            }
            else if(optionsMenu.activeSelf) {
                optionsMenu.GetComponent<OptionsMenuActions>().Back();
            }
            else if(tutorial1.activeSelf) {
                tutorial1.GetComponent<TutorialMenuActions>().Resume();
            }
            else if(tutorial2.activeSelf) {
                tutorial2.GetComponent<TutorialMenuActions>().Resume();
            }
            else if(tutorial3.activeSelf) {
                tutorial3.GetComponent<TutorialMenuActions>().Resume();
            }
            else if(pauseMenu.activeSelf) {
                pauseMenu.GetComponent<PauseMenuActions>().Resume();
            }
            else {
                pauseMenu.GetComponent<PauseMenuActions>().Pause();
            }
        }

        switch(GameManager.instance.tutorial) {
        case 0:
            if(GameManager.instance.CombatCounter > 0) {
                tutorial1.GetComponent<TutorialMenuActions>().UpdateControls();
                tutorial1.GetComponent<TutorialMenuActions>().Pause();
                GameManager.instance.tutorial = 2;
            }
            break;
        case 1:
            if(GameManager.instance.CombatCounter > 0) {
                GameManager.instance.tutorial++;
            }
            break;
        case 2:
            if(!GameManager.instance.player.GetComponent<Health>().alive) {
                GameManager.instance.tutorial = 1;
            }
            else if(GameManager.instance.CombatCounter == 0) {
                tutorial2.GetComponent<TutorialMenuActions>().UpdateControls();
                tutorial2.GetComponent<TutorialMenuActions>().Pause();
                GameManager.instance.tutorial++;
            }
            break;
        case 3:
            if(GameManager.instance.CombatCounter > 0) {
                tutorial3.GetComponent<TutorialMenuActions>().UpdateControls();
                tutorial3.GetComponent<TutorialMenuActions>().Pause();
                GameManager.instance.tutorial++;
            }
            break;
        }
    }

    public void GameOver() {
        GameManager.instance.isStopped = true;
        Time.timeScale = 0.0f;
        gameOverMenu.SetActive(true);
    }
}
