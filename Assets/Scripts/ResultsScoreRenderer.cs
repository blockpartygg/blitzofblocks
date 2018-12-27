using UnityEngine;
using TMPro;
using System;

public class ResultsScoreRenderer : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] TMP_Text text = null;

    void Update() {
        text.text = scoreManager.Points.ToString();
    }
}