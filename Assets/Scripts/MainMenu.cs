using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        PlayerPrefs.SetString("Score", "0");
        SceneManager.LoadScene(2);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
