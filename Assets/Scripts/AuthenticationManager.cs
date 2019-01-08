using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class AuthenticationManager : MonoBehaviour {
    public string DisplayName;

    void Start() {
        if(!PlayFabClientAPI.IsClientLoggedIn()) {
            string customId;
            if (!PlayerPrefs.HasKey("customId")) {
                Guid guid = Guid.NewGuid();
                customId = guid.ToString();
                PlayerPrefs.SetString("customId", customId);
            }
            else {
                customId = PlayerPrefs.GetString("customId");
            }

            var request = new LoginWithCustomIDRequest {
                CustomId = customId,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() {
                    GetPlayerProfile = true,
                    ProfileConstraints = new PlayerProfileViewConstraints() {
                        ShowDisplayName = true
                    }
                }
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
        }
        else {
            if(string.IsNullOrEmpty(DisplayName)) {
                PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest() {
                    ProfileConstraints = new PlayerProfileViewConstraints() {
                        ShowDisplayName = true
                    }
                }, OnGetProfileSuccess, OnError);
            }
        }
    }

    void OnLoginSuccess(LoginResult result) {
        DisplayName = result.InfoResultPayload.PlayerProfile.DisplayName;
    }

    void OnGetProfileSuccess(GetPlayerProfileResult result) {
        DisplayName = result.PlayerProfile.DisplayName;
    }

    void OnError(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void SetPlayerName(string playerName, Action<UpdateUserTitleDisplayNameResult> resultCallback, Action<PlayFabError> errorCallback) {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest() {
            DisplayName = playerName
        },
        resultCallback,
        errorCallback
        );
    }
}