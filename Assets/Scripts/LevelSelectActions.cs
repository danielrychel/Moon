using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectActions : MonoBehaviour {
    public GameObject backMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevel(int level) {
        GameManager.instance.GetComponent<LevelManagement>().SetLevel(level);
    }

    public void Back() {
        backMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
