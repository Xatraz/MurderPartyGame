using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public Timer timer;

    public void LoadLevel (int sceneIndex)
    {
        //StartCoroutine(LoadAsynchronously(sceneIndex));
        SceneManager.LoadScene(sceneIndex);
        
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
        //Debug.Log("test");
        //PlayerPrefs.SetInt("gameStarting", 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
