using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuActions : MonoBehaviour {
    public GameObject mainMenu;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        mixer.SetFloat("musicVol", musicVolSlider.value);
        mixer.SetFloat("sfxVol", sfxVolSlider.value);
    }

    public void Back() {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
