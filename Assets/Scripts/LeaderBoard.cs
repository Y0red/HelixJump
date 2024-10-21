using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]TMP_Text scoreText;
    void OnEnable()
    {
        scoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
    }
}
