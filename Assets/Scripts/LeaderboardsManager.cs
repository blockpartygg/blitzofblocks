using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LeaderboardsManager : MonoBehaviour {
    public List<PlayerLeaderboardEntry> Entries;

    async void Start() {
        // Wait for the client to be logged in before requesting the leaderboard
        while(!PlayFabClientAPI.IsClientLoggedIn()) {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest() {
            StatisticName = "Score",
            StartPosition = 0,
        },
        OnSuccess,
        OnError);
    }

    void OnSuccess(GetLeaderboardResult result) {
        Entries = result.Leaderboard;
    }

    void OnError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }
}
