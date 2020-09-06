using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField]
    GameOverTextViewer m_gameOverTextViewer = null;

    

    
    
    void OnTriggerEnter(Collider col)
    {
        m_gameOverTextViewer.GameOverTextView();
       
    }
}