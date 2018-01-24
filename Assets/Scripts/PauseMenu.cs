using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseScreen;
    public string cancel = "Cancel";

    private bool inputCancel;

    private void Update()
    {
        inputCancel = Input.GetButtonDown(cancel);
        if (inputCancel == true)
        {
            Toggle();
        }    
    }

    public void Toggle()
    {
        PauseScreen.SetActive(!PauseScreen.activeSelf);

        if (PauseScreen.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
