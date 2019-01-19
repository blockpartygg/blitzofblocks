using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardsManager : MonoBehaviour {
    public List<PlayerLeaderboardEntry> Entries;

    IEnumerator Start() {
        // Wait for the client to be logged in before requesting the leaderboard
        while(!PlayFabClientAPI.IsClientLoggedIn()) {
            yield return new WaitForSeconds(1);
        }

        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest() {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 100
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
