using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    public List<string> Scenes;
    public Slider slider;
    public GameObject canvas;

    public void ViewLoadScene(int i)
    {
        canvas.SetActive(true);
        StartCoroutine(LoadYourAsyncScene(i));
    }
    IEnumerator LoadYourAsyncScene(int i)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes[i]);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            yield return null;
        }
    }
}
