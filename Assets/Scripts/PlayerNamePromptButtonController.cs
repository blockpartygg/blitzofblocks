﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayerNamePromptButtonController : MonoBehaviour {
    [SerializeField] TMP_InputField inputField = null;
    [SerializeField] AuthenticationManager authenticationManager = null;
    [SerializeField] StatisticsManager statisticsManager = null;
    [SerializeField] SceneFader sceneFader = null;
    Button button;

    void Awake() {
        button = GetComponent<Button>();        
    }

    public void Submit() {
        authenticationManager.SetPlayerName(inputField.text, OnUpdateNameResult, OnError);
    }

    void OnUpdateNameResult(UpdateUserTitleDisplayNameResult result) {
        statisticsManager.UpdateStatistics(OnUpdateStatisticsResult, OnError);
    }

    void OnUpdateStatisticsResult(UpdatePlayerStatisticsResult result) {
        sceneFader.FadeToScene("Leaderboards");
    }

    void OnError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    void Update() {
        button.interactable = !string.IsNullOrEmpty(inputField.text);
    }
}