using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class death_screen : MonoBehaviour
{
    public GameObject[] deathObjects;
    public Health hp;
    public Button restart;
    public Button menu;
    // Start is called before the first frame update
    void Start()
    {
        deathObjects = GameObject.FindGameObjectsWithTag("death_ui");
        restart.onClick.AddListener(restart_level);
        menu.onClick.AddListener(to_main_menu);
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(false);
        }
}

    // Update is called once per frame
    void Update()
    {
        if (!hp.alive) {
            foreach (GameObject g in deathObjects)
            {
                g.SetActive(true);
            }
            //Scene thisScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(thisScene.name);
        }
    }

    void restart_level() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    void to_main_menu() {
        SceneManager.LoadScene("Main Menu");
    }
}
