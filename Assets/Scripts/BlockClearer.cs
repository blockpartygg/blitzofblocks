using UnityEngine;

public class BlockClearer : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] FloatReference clearDuration = null;
    [SerializeField] BlockEmptier emptier = null;
    // public Score Score; // TODO: Convert this to a GameEvent that Score listens to
    // public AudioSource AudioSource; // Convert this to a GameEventListener
    float delayElapsed;
    public float DelayDuration;
    public float Elapsed;
    // public float Pitch;

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
                // Score.ScoreMatch();
                // AudioSource.pitch = Pitch;
                // AudioSource.Play();
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