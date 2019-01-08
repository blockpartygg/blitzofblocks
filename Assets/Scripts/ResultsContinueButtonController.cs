using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class ResultsContinueButtonController : MonoBehaviour {
    [SerializeField] AuthenticationManager authenticationManager = null;
    [SerializeField] StatisticsManager statisticsManager = null;
    [SerializeField] SceneFader sceneFader = null;

    public void Continue() {
        // If the logged in player doesn't have a display name, load the player name entry scene.
        if(string.IsNullOrEmpty(authenticationManager.DisplayName)) {
            sceneFader.FadeToScene("PlayerNameEntry");
        }

        // Otherwise, update stats from the latest game, and load the leaderboards scene.
        else {
            statisticsManager.UpdateStatistics(OnUpdateStatisticsResult, OnError);
        }
    }

    void OnUpdateStatisticsResult(UpdatePlayerStatisticsResult result) {
        sceneFader.FadeToScene("Leaderboards");
    }

    void OnError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }
}