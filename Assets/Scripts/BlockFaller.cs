using UnityEngine;

public class BlockFaller : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] FloatReference fallDelayDuration = null;
    [SerializeField] FloatReference fallDuration = null;
    float delayElapsed;
    public float Elapsed;
    Block target;
    public bool JustFell;
    public bool JustLanded;

    public void SetTarget(Block block) {
        target = block;
    }
    
    public void Fall() {
        block.State = BlockState.WaitingToFall;
        delayElapsed = 0f;
    }

    public void ContinueFalling() {
        FinishWaitingToFall();
    }

    void FinishWaitingToFall() {
        block.State = BlockState.Falling;
        Elapsed = 0f;
    }

    void Update() {
        if(block.State == BlockState.WaitingToFall) {
            delayElapsed += Time.deltaTime;

            if(delayElapsed >= fallDelayDuration.Value) {
                FinishWaitingToFall();
            }
        }

        if(block.State == BlockState.Falling) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= fallDuration.Value) {
                if(target != null) {
                    target.Type = block.Type;
                    target.State = BlockState.Falling;
                    target.Faller.JustFell = true;
                    target.Faller.Elapsed = 0;
                    target.Chainer.SetChainEligibility(block.Chainer.ChainEligible);
                }
                
                block.State = BlockState.Empty;
                block.Type = -1;
                block.Chainer.SetChainEligibility(false);
            }
        }
    }
}