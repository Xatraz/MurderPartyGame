using UnityEngine;
using UnityEngine.UI;

public class KillerChooserScript : MonoBehaviour {

    public Image p1_Image, p2_Image, p3_Image, p4_Image;
    public GameObject p1, p2, p3, p4;
    [Header("Arrays")]
    public Image[] images;
    public GameObject[] players;
    [Header("Sprites")]
    public Sprite murdererSprite;
    [Header("Int")]
    public int randomPlayerNr;

    void Awake ()
    {
        randomPlayerNr = Random.Range(0, players.Length);
        players[randomPlayerNr].GetComponent<PlayerControllerNormal>().enabled = false;   //Disable Normal player controller
        players[randomPlayerNr].GetComponent<PlayerControllerKiller>().enabled = true;    //Enable Killer player controller
        players[randomPlayerNr].GetComponent<SpriteRenderer>().sprite = murdererSprite;   //Change Killer player Sprite
        images[randomPlayerNr].gameObject.SetActive(true);                                //Activate Killer Image in UI
        players[randomPlayerNr].gameObject.layer = 2;                                     //Change the Killers layer to Ignore Raycast
        players[randomPlayerNr].gameObject.tag = "PlayerKiller";                          //Change the Killers tag to PlayerKiller
    }
}