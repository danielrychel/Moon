using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_end : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Rigidbody2D player;
    public GameObject tutorial;
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
        Vector2 control_vec = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
        Vector2 player_vec = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 controls_to_player = player_vec - control_vec;
        float distance = Mathf.Abs(Vector2.Distance(control_vec, player_vec));

        if (distance <= 3)
        {
            if (Input.GetButtonDown("Use"))
            {
                GameManager.instance.GetComponent<LevelManagement>().NextLevel();
            }
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
