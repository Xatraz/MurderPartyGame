using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsKiller : MonoBehaviour {

    public int power = 1;               //What powerup will me triggered
    public float boostSpeed = 2f;
    public GameObject gameMaster;

    private int randomPlayerNr;
    private GameObject[] players;

    void Start()
    {
        //Getting player gameobject
        randomPlayerNr = gameMaster.GetComponent<KillerChooserScript>().randomPlayerNr;
        players = gameMaster.GetComponent<KillerChooserScript>().players;
        Debug.Log(players[randomPlayerNr]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerKiller")
        {
            switch (power)
            {
                case 1:                             //Powerup 1
                    WallMove();
                    Invoke("StopWallMove", 2);
                    break;
                case 2:                             //Powerup 2
                    Boost();
                    Invoke("StopBoost", 2);
                    break;
                case 3:                             //Powerup 3

                    break;
            }
        }
    }

    //The Killer can move through walls
    void WallMove()
    {
        players[randomPlayerNr].GetComponent<Collider2D>().isTrigger = true;
        Debug.Log("WallMove");
    }
    void StopWallMove()
    {
        players[randomPlayerNr].GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("StopWallMove");
    }

    //The Killers will move faster
    void Boost()
    {
        players[randomPlayerNr].GetComponent<PlayerControllerKiller>().speed *= boostSpeed;
        Debug.Log("Boost");
    }
    void StopBoost()
    {
        players[randomPlayerNr].GetComponent<PlayerControllerKiller>().speed /= boostSpeed;
        Debug.Log("StopBoost");
    }
}