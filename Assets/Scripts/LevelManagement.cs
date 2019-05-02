using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    public string[] levels;
    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        currentLevel = -1;
        for(int i=0; i<levels.Length; i++) {
            if(levels[i] == scene) {
                currentLevel = i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ResetLevel() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        GameManager.instance.isStopped = false;
    }

    public void NextLevel() {
        currentLevel = (currentLevel + 1) % levels.Length;
        SceneManager.LoadScene(levels[currentLevel]);
        //if(currentLevel == 2)
        //{
        //    GameManager.instance.SetBoss();
        //}
        GameManager.instance.isStopped = false;
    }

    public void ExitMainMenu() {
        currentLevel = 0;
        SceneManager.LoadScene("Main Menu");
        GameManager.instance.isStopped = false;
    }
}
