using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCaculation : MonoBehaviour
{
    public Text scoreText;
    // Update is called once per frame
    void FixedUpdate()
    {
        // 2 seconds 1 point
        scoreText.text = "" + (int) Time.timeSinceLevelLoad;
        PlayerPrefs.SetString("Score", scoreText.text);
    }
}
