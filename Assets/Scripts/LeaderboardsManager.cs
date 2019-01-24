using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardsManager : MonoBehaviour {
    public List<PlayerLeaderboardEntry> ScoreWebEntries;
    public List<PlayerLeaderboardEntry> ScoreMobileEntries;
    public List<PlayerLeaderboardEntry> BlocksMatchedWebEntries;
    public List<PlayerLeaderboardEntry> BlocksMatchedMobileEntries;

    IEnumerator Start() {
        // Wait for the client to be logged in before requesting the leaderboard
        while(!PlayFabClientAPI.IsClientLoggedIn()) {
            yield return new WaitForSeconds(1);
        }

        GetLeaderboard("Score", result => ScoreWebEntries = result.Leaderboard);
        GetLeaderboard("ScoreMobile", result => ScoreMobileEntries = result.Leaderboard);
        GetLeaderboard("BlocksMatched", result => BlocksMatchedWebEntries = result.Leaderboard);
        GetLeaderboard("BlocksMatchedMobile", result => BlocksMatchedMobileEntries = result.Leaderboard);
    }

    public void GetLeaderboard(string statisticName, Action<GetLeaderboardResult> resultCallback) {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest() {
            StatisticName = statisticName,
            StartPosition = 0,
            MaxResultsCount = 100
        },
        resultCallback,
        OnError);
    }

    void OnError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }
}
