using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    public GameObject endScreen;

    private int playersAmmount;
    private int playersDead;
    private bool EndScreenActive = false;

    private void Update()
    {
        playersAmmount = gameObject.GetComponent<KillerChooserScript>().players.Length;
        playersDead = 0;
        float timeLeft = gameObject.GetComponent<Timer>().gameTime;
        int playersAlive = playersAmmount - playersDead;
        if ((playersAlive <= 2 || timeLeft < 1) && EndScreenActive == false)
        {
            EndScreenToggle();
            EndScreenActive = true;
        }
    }

    public void EndScreenToggle()
    {
        endScreen.SetActive(!endScreen.activeSelf);

        if (endScreen.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
