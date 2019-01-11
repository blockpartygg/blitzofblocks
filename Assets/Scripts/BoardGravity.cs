using UnityEngine;

public class BoardGravity : MonoBehaviour {
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] MatchDetector matchDetector = null;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;
    [SerializeField] AudioCue audioCue = null;
    [SerializeField] AudioSource audioSource = null;

    void Update() {
        for(int column = 0; column < columns.Value; column++) {
            bool emptyBlockDetected = false;
            
            // Traverse from bottom to top to detect whether there are any empty blocks underneath the current one
            for(int row = 0; row < rows.Value + 1; row++) {
                if(blockManager.Blocks[column, row].State == BlockState.Empty) {
                    emptyBlockDetected = true;
                }

                // If the current block is idle and there's atleast one empty one underneath it, make it fall
                if(blockManager.Blocks[column, row].State == BlockState.Idle && emptyBlockDetected) {
                    blockManager.Blocks[column, row].Faller.SetTarget(blockManager.Blocks[column, row - 1]);
                    blockManager.Blocks[column, row].Faller.Fall();
                }

                // If the current block just fell...
                if(blockManager.Blocks[column, row].Faller.JustFell) {
                    // If the block underneath (assuming there is one) is empty or currently falling
                    if(row > 0 && (blockManager.Blocks[column, row - 1].State == BlockState.Empty || blockManager.Blocks[column, row - 1].State == BlockState.Falling)) {
                        // Make them continue falling immediately
                        blockManager.Blocks[column, row].Faller.SetTarget(blockManager.Blocks[column, row - 1]);
                        blockManager.Blocks[column, row].Faller.ContinueFalling();
                    }
                    // Otherwise, land the block and request a match detection
                    else {
                        blockManager.Blocks[column, row].State = BlockState.Idle;
                        blockManager.Blocks[column, row].Faller.JustLanded = true;
                        matchDetector.RequestMatchDetection(blockManager.Blocks[column, row]);
                        audioCue.Play(audioSource);
                    }

                    blockManager.Blocks[column, row].Faller.JustFell = false;
                }
            }
        }
    }
}