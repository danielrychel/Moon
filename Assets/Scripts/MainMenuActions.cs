using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour {
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame() {
        levelSelect.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenOptions() {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenControls() {
        controlsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
