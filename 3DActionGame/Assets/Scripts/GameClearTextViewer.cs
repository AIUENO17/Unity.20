using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameClearTextViewer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_TextMeshProUGUI = null;

     

    string TextMeshProUGUI = ("GameClear!");

    public void ClearTextView()
    {
        m_TextMeshProUGUI.text = TextMeshProUGUI;
        Time.timeScale = 0f;
    }
}