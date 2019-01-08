using UnityEngine;
using PlayFab.ClientModels;
using System;
using System.Threading.Tasks;
using TMPro;

public class LeaderboardView : MonoBehaviour {
    [SerializeField] LeaderboardsManager leaderboardManager = null;
    [SerializeField] Transform content = null;
    [SerializeField] GameObject leaderboardEntryPrefab = null;

    async void Start() {
        while(leaderboardManager.Entries.Count == 0) {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        PopulateLeaderboard();
    }

    void PopulateLeaderboard() {
        // Destroy any existing game objects that are children to the leaderboard content object
        foreach(Transform child in content) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (PlayerLeaderboardEntry entry in leaderboardManager.Entries) {
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
