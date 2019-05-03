using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlsMenuActions : MonoBehaviour {
    public GameObject backMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
    }

    public void Back() {
        backMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
