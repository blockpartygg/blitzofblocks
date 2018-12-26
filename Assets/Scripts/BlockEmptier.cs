using UnityEngine;

public class BlockEmptier : MonoBehaviour {
    [SerializeField] Block block = null;
    float delayElapsed;
    public float DelayDuration;

    public void Empty() {
        block.State = BlockState.WaitingToEmpty;
        delayElapsed = 0f;
    }

    void Update() {
        if(block.State == BlockState.WaitingToEmpty) {
            delayElapsed += Time.deltaTime;

            if(delayElapsed >= DelayDuration) {
                block.State = BlockState.Empty;
                block.Type = -1;
                // Block.Chainer.JustEmptied = true;
            }
        }
    }
}