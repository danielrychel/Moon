using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_screen : MonoBehaviour {

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void restart_level() {
        GameManager.instance.GetComponent<LevelManagement>().ResetLevel();
    }

    public void to_main_menu() {
        GameManager.instance.GetComponent<LevelManagement>().ExitMainMenu();
    }
}
