using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTutorial : MonoBehaviour
{
    public door_control door;
    public GameObject tutorial;
    public float range;
    public Image prompt;
    public Sprite keyboard;
    public Sprite joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(door.getDistanceSqr() <= range*range && !door.switched) {
            tutorial.SetActive(true);
        }
        else {
            tutorial.SetActive(false);
        }

        if(GameManager.instance.useKeyboard)
            prompt.sprite = keyboard;
        else
            prompt.sprite = joystick;
    }
}
