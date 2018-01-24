using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public bool isTriggered = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isTriggered = false;
        }
    }
}
