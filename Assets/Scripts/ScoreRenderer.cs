using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class ScoreRenderer : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] TMP_Text text = null;

    void Start() {
        UpdateText();
    }

    public void UpdateText() {
        text.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        text.transform.DOScale(1, 0.5f);
        DOTween.To(value => text.text = Mathf.Round(value).ToString(), float.Parse(text.text), scoreManager.Points, 0.5f);
    }
}