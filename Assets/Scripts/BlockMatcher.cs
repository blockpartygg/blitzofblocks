using UnityEngine;

public class BlockMatcher : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] FloatReference clearDelayInterval = null;
    [SerializeField] FloatReference emptyDelayInterval = null;
    [SerializeField] FloatReference matchDuration = null;
    [SerializeField] BlockClearer clearer = null;
    [SerializeField] BlockEmptier emptier = null;
    public float Elapsed;

    public void Match(int matchedBlockCount, int delayCounter) {
        block.State = BlockState.Matched;
        Elapsed = 0f;
        clearer.DelayDuration = (matchedBlockCount - delayCounter) * clearDelayInterval.Value;
        emptier.DelayDuration = delayCounter * emptyDelayInterval.Value;
        clearer.Pitch = 0.75f + (3 - delayCounter) * 0.25f;
    }

    void Update() {
        if(block.State == BlockState.Matched) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= matchDuration.Value) {
                clearer.Clear();
            }
        }
    }
}