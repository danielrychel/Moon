using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutorial : MonoBehaviour
{
    public door_control door;
    public GameObject tutorial;
    public float range;

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
    }
}
