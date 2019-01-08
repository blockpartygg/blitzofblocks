using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StatisticsManager : MonoBehaviour {
    [SerializeField] ScoreManager scoreManager = null;

    IEnumerator Start() {
        // Wait for the client to be logged in before requesting their stats
        while (!PlayFabClientAPI.IsClientLoggedIn()) {
            yield return new WaitForSeconds(1);
        }

        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest() {
            StatisticNames = new List<string> {
                "Score",
                "BlocksMatched"
            }
        }, 
        OnSuccess,
        OnError);
    }

    void OnSuccess(GetPlayerStatisticsResult result) {
        foreach(StatisticValue value in result.Statistics) {
            // Todo: cache stats so you can use them immediately as needed
        }
    }

    void OnError(PlayFabError error) {
        Debug.Log(error.GenerateErrorReport());
    }

    public void UpdateStatistics(Action<UpdatePlayerStatisticsResult> resultCallback, Action<PlayFabError> errorCallback) {
        List<StatisticUpdate> updates = new List<StatisticUpdate>();
        
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest() {
            Statistics = new List<StatisticUpdate>() {
                new StatisticUpdate() {
                    StatisticName = "Score",
                    Value = scoreManager.Points
                }
            }
        },
        resultCallback,
        errorCallback
        );
    }
}
