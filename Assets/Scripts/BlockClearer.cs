using UnityEngine;

public class BlockClearer : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] FloatReference clearDuration = null;
    [SerializeField] BlockEmptier emptier = null;
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] AudioCue audioCue = null;
    [SerializeField] AudioSource audioSource = null;
    float delayElapsed;
    public float DelayDuration;
    public float Elapsed;
    public float Pitch;

    public void Clear() {
        block.State = BlockState.WaitingToClear;
        delayElapsed = 0f;
    }

    void Update() {
        if(block.State == BlockState.WaitingToClear) {
            delayElapsed += Time.deltaTime;
            
            if(delayElapsed >= DelayDuration) {
                block.State = BlockState.Clearing;
                Elapsed = 0f;
                scoreManager.ScoreMatch();
                audioCue.Pitch = Pitch;
                audioCue.Play(audioSource);
            }
        }

        if(block.State == BlockState.Clearing) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= clearDuration.Value) {
                emptier.Empty();
            }
        }
    }
}