using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] AudioCue titleCue = null;
    [SerializeField] AudioCue inGameCue = null;
    [SerializeField] AudioCue postGameCue = null;
    [SerializeField] AudioSource audioSource = null;
    AudioCue currentCue;

    public static MusicPlayer Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }       
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        switch(SceneManager.GetActiveScene().name) {
            case "Title":
                SetMusic(titleCue);
                break;
            case "Game":
                // Stop whatever music is playing, and let GameManager start the music when gameplay starts
                if (currentCue != inGameCue && audioSource.isPlaying) {
                    audioSource.Stop();
                }
                break;
            case "Results":
            case "PlayerNameEntry":
            case "Leaderboard":
                SetMusic(postGameCue);
                break;
        }
    }

    public void SetMusic(AudioCue cue) {
        if(currentCue != cue) {
            currentCue = cue;
            currentCue.Play(audioSource);
        }
    }

    public void Stop() {
        audioSource.Stop();
    }
}
