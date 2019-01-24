using UnityEngine;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class LeaderboardView : MonoBehaviour {
    [SerializeField] LeaderboardsManager leaderboardManager = null;
    [SerializeField] Transform content = null;
    [SerializeField] GameObject leaderboardEntryPrefab = null;

    IEnumerator Start() {
        while(leaderboardManager.ScoreWebEntries.Count == 0 ||
            leaderboardManager.ScoreMobileEntries.Count == 0 ||
            leaderboardManager.BlocksMatchedWebEntries.Count == 0 ||
            leaderboardManager.BlocksMatchedMobileEntries.Count == 0) {
            yield return new WaitForSeconds(1);
        }

        bool isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        PopulateLeaderboard(isMobile ? leaderboardManager.ScoreMobileEntries : leaderboardManager.ScoreWebEntries);
    }

    public void HandleLeaderboardDropdownValueChanged(int value) {
        switch (value) {
            case 0: // Score (Web)
                leaderboardManager.GetLeaderboard("Score", result => {
                    leaderboardManager.ScoreWebEntries = result.Leaderboard;
                    PopulateLeaderboard(leaderboardManager.ScoreWebEntries);
                });
                break;
            case 1: // Blocks Matched (Web)
                leaderboardManager.GetLeaderboard("ScoreMobile", result => {
                    leaderboardManager.ScoreWebEntries = result.Leaderboard;
                    PopulateLeaderboard(leaderboardManager.BlocksMatchedWebEntries);
                });
                break;
            case 2: // Score (Mobile)
                leaderboardManager.GetLeaderboard("BlocksMatched", result => {
                    leaderboardManager.ScoreWebEntries = result.Leaderboard;
                    PopulateLeaderboard(leaderboardManager.ScoreMobileEntries);
                });
                break;
            case 3:
                leaderboardManager.GetLeaderboard("BlocksMatchedMobile", result => {
                    leaderboardManager.ScoreWebEntries = result.Leaderboard;
                    PopulateLeaderboard(leaderboardManager.BlocksMatchedMobileEntries);
                });
                break;
        }
    }

    void PopulateLeaderboard(List<PlayerLeaderboardEntry> entries) {
        // Destroy any existing game objects that are children to the leaderboard content object
        foreach(Transform child in content) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (PlayerLeaderboardEntry entry in entries) {
            GameObject leaderboardEntry = Instantiate(leaderboardEntryPrefab, content);
            GameObject panel = leaderboardEntry.transform.Find("Panel").gameObject;

            GameObject rank = panel.transform.Find("Rank").gameObject;
            TMP_Text rankText = rank.GetComponent<TMP_Text>();
            rankText.text = (entry.Position + 1).ToString();

            GameObject name = panel.transform.Find("Name").gameObject;
            TMP_Text nameText = name.GetComponent<TMP_Text>();
            nameText.text = entry.DisplayName;

            GameObject score = panel.transform.Find("Score").gameObject;
            TMP_Text scoreText = score.GetComponent<TMP_Text>();
            scoreText.text = entry.StatValue.ToString();
        }
    }
}
