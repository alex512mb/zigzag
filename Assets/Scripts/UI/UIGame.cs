using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    public Text textScores;


    private void Awake()
    {
        if (textScores == null)
            Debug.LogWarning("text scores does not assigned");
    }
    public void SetScore(int countScores)
    {
        textScores.text = "Scores: " + countScores;
    }
}
