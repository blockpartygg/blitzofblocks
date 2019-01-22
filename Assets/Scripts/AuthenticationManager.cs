using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class AuthenticationManager : MonoBehaviour {
    public string DisplayName;

    void Start() {
        if(!PlayFabClientAPI.IsClientLoggedIn()) {
            var infoRequestParameters = new GetPlayerCombinedInfoRequestParams() {
                GetPlayerProfile = true,
                ProfileConstraints = new PlayerProfileViewConstraints() {
                    ShowDisplayName = true
                }
            };

            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                var request = new LoginWithIOSDeviceIDRequest() {
                    DeviceId = SystemInfo.deviceUniqueIdentifier,
                    DeviceModel = SystemInfo.deviceModel,
                    OS = SystemInfo.operatingSystem,
                    CreateAccount = true,
                    InfoRequestParameters = infoRequestParameters
                };

                PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnError);
            }
            else if(Application.platform == RuntimePlatform.Android) {
                var request = new LoginWithAndroidDeviceIDRequest() {
                    AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
                    AndroidDevice = SystemInfo.deviceModel,
                    OS = SystemInfo.operatingSystem,
                    CreateAccount = true,
                    InfoRequestParameters = infoRequestParameters
                };

                PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnError);
            }
            else {
                string customId;
                if (!PlayerPrefs.HasKey("customId")) {
                    Guid guid = Guid.NewGuid();
                    customId = guid.ToString();
                    PlayerPrefs.SetString("customId", customId);
                }
                else {
                    customId = PlayerPrefs.GetString("customId");
                }

                var request = new LoginWithCustomIDRequest() {
                    CustomId = customId,
                    CreateAccount = true,
                    InfoRequestParameters = infoRequestParameters
                };
                PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
            }
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
        if(result != null) {
            if(result.InfoResultPayload != null) {
                if(result.InfoResultPayload.PlayerProfile != null) {
                    DisplayName = result.InfoResultPayload.PlayerProfile.DisplayName;
                }
                else {
                    Debug.LogError("result.InfoResultPayload.PlayerProfile is null");
                }
            }
            else {
                Debug.LogError("result.InfoResultPayload is null");
            }
        }
        else {
            Debug.Log("result is null");
        }
    }

    void OnGetProfileSuccess(GetPlayerProfileResult result) {
        if(result != null) {
            if(result.PlayerProfile != null) {
                DisplayName = result.PlayerProfile.DisplayName;
            }
            else {
                Debug.LogError("result.PlayerProfile is null");
            }
        }
        else {
            Debug.Log("result is null");
        }
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