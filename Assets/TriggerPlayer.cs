using UnityEngine;

public class TriggerPlayer : MonoBehaviour {
    public int playersInTrigger = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playersInTrigger += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playersInTrigger -= 1;
        }
    }
}