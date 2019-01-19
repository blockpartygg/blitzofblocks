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

#if !UNITY_EDITOR && UNITY_IOS
            //string deviceId = UnityEngine.iOS.Device.vendorIdentifier;
            var request = new LoginWithIOSDeviceIDRequest() {
                //DeviceId = deviceId,
                DeviceId = SystemInfo.deviceUniqueIdentifier,
                DeviceModel = SystemInfo.deviceModel,
                OS = SystemInfo.operatingSystem,
                TitleId = "DF41",
                CreateAccount = true,
                InfoRequestParameters = infoRequestParameters
            };
            Debug.Log("Attempting login");
            try {

            PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnError);  
            } catch(Exception e) {
            Debug.Log("Caught error:");
            Debug.Log(e);
            }
#else
            string customId;
            if (!PlayerPrefs.HasKey("customId")) {
                Guid guid = Guid.NewGuid();
                customId = guid.ToString();
                PlayerPrefs.SetString("customId", customId);
            }
            else {
                customId = PlayerPrefs.GetString("customId");
                Debug.Log("CustomId is " + customId);
            }

            var request = new LoginWithCustomIDRequest() {
                CustomId = customId,
                CreateAccount = true,
                InfoRequestParameters = infoRequestParameters
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
#endif
        }
        else {
            Debug.Log("Already logged in. Getting profile");
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
        Debug.Log("OnLoginSuccess");
        if(result != null) {
            if(result.InfoResultPayload != null) {
                if(result.InfoResultPayload.PlayerProfile != null) {
                    Debug.Log("Successfully logged in");
                    Debug.Log("Setting display name to " + result.InfoResultPayload.PlayerProfile.DisplayName);
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
        Debug.Log("OnGetProfileSuccess");
        if(result != null)
        {
            if(result.PlayerProfile != null)
            {
                DisplayName = result.PlayerProfile.DisplayName;
            }
            else
            {
                Debug.LogError("result.PlayerProfile is null");
            }
        }
        else
        {
            Debug.Log("result is null");
        }
    }

    void OnError(PlayFabError error) {
        Debug.Log("OnError");
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