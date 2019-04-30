using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;     //Allows other scripts to call functions from SoundManager.
    [SerializeField] private MusicManager musicManager;
    public bool inCombat;
    public bool isStopped;
    public bool isBoss;
    public bool isMenu;
    public GameObject player;
    [SerializeField] public int CombatCounter;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        CombatCounter = 0;
        inCombat = false;
        isStopped = false;
    }
    void Update()
    {
        if (player != null)
        {
            if (CombatCounter > 0) inCombat = true;
            else inCombat = false;

            if (inCombat & !isBoss & player.GetComponent<Health>().alive)
            {
                SetCombat();
            }
            else if (player.GetComponent<Health>().alive & !isBoss)
            {
                ExitCombat();
            }

            if (!player.GetComponent<Health>().alive)
            {
                SetDeath();
                CombatCounter = 0;
            }            
            if (MusicManager.MusicState.Death == musicManager.current_music && player.GetComponent<Health>().alive)
            {
                CombatCounter = 0;
                ExitCombat();
            }

        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
        if(player == null)
        {
            //Main Menu
            SetMenu();
        }

    }

    /// <summary>
    /// incr > 0 increases aggro count by one, incr <= 0 decreases aggro count by one
    /// </summary>
    /// <param name="incr"></param>
    public void AggroCounter(int incr, bool aggroed)
    {
        if (incr > 0 && !aggroed)
            CombatCounter++;
        else if (CombatCounter > 0 && incr <= 0)
            CombatCounter--;
    }


    private void SetCombat()
    {
        if (MusicManager.MusicState.Combat == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Combat;
    }

    private void ExitCombat()
    {
        if (MusicManager.MusicState.OutOfCombat == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.OutOfCombat;
    }

    public void SetBoss()
    {
        if (MusicManager.MusicState.Boss == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Boss;
    }

    private void SetDeath()
    {
        if (MusicManager.MusicState.Death == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Death;
    }


    private void SetMenu()
    {
        if (MusicManager.MusicState.Menu == musicManager.current_music) return;
        musicManager.FadeMusic();
        musicManager.next_music = MusicManager.MusicState.Menu;
    }
}
