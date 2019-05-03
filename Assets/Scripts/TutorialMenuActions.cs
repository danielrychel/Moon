using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuActions : MonoBehaviour {
    public Image img1, img2;
    public Sprite img1Key, img1Joy, img2Key, img2Joy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControls();
    }

    public void UpdateControls() {
        if(GameManager.instance.useKeyboard) {
            img1.sprite = img1Key;
            img2.sprite = img2Key;
            if(img2Key)
                img2.color = new Color(1, 1, 1, 1);
            else
                img2.color = new Color(0, 0, 0, 0);
        }
        else {
            img1.sprite = img1Joy;
            img2.sprite = img2Joy;
            if(img2Joy)
                img2.color = new Color(1, 1, 1, 1);
            else
                img2.color = new Color(0, 0, 0, 0);
        }
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
}
