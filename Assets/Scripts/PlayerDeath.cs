using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public Sprite deadSprite;
    public float deadDespawnTime = 1f;
    public GameObject playerDeadUI;

	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
        GetComponent<PlayerControllerNormal>().movementVector = new Vector2(0, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        playerDeadUI.SetActive(true);
        GetComponent<PlayerControllerNormal>().enabled = false;
        Invoke("DisablePlayer", deadDespawnTime);
    }

    public void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}
