using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Text gameOverText;

    private void Start()
    {
        gameOverText.text = "Score: " + PlayerPrefs.GetString("Score");
    }

}
