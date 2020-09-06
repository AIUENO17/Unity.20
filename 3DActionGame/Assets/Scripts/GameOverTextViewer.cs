using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextViewer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI t_TextMeshProUGUI = null;

    string TextMeshProUGUI = ("GameOver!");



    public void GameOverTextView()
    {
        t_TextMeshProUGUI.text = TextMeshProUGUI;
        Time.timeScale = 0f;
    }
}

