using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCheck : MonoBehaviour {

    public float maxTimeNotMoving = 2f;
    private bool inTrigger;
    private GameObject colGO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inTrigger = true;
        colGO = collision.gameObject;
        Hallo();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        inTrigger = false;
    }

    IEnumerator Hallo()
    {
        yield return new WaitForSeconds(maxTimeNotMoving);
        if (inTrigger == true)
        {
            colGO.GetComponent<PlayerDeath>().enabled = true;
        }
        yield return null;
    }
}
