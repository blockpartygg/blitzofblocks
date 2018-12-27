using UnityEngine;
using TMPro;
using System;

public class ScoreRenderer : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] TMP_Text text = null;

    void Start() {
        UpdateText();
    }

    public void UpdateText() {
        text.text = scoreManager.Points.ToString();
    }
}