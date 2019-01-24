using UnityEngine;
using TMPro;

public class LeaderboardDropdownController : MonoBehaviour {
    TMP_Dropdown dropdown;

    void Awake() {
        dropdown = GetComponent<TMP_Dropdown>();    
    }

    void Start() {
        bool isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        dropdown.value = isMobile ? 2 : 0;
    }
}
