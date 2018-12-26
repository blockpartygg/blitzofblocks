using UnityEngine;

public class BlockManager_TopRowCreator : MonoBehaviour {
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;

    void Update() {
        // Note: This executes after BoardGravity so that falling blocks land correctly
        for(int column = 0; column < columns.Value; column++) {
            if(blockManager.Blocks[column, rows.Value].State == BlockState.Empty) {
                blockManager.Blocks[column, rows.Value].Type = blockManager.GetRandomBlockType(column, rows.Value);

                if(blockManager.Blocks[column, rows.Value - 1].State == BlockState.Idle) {
                    blockManager.Blocks[column, rows.Value].State = BlockState.Idle;
                    // blockManager.Blocks[column, rows.Value].Chainer.ChainEligible = false;
                }

                if(blockManager.Blocks[column, rows.Value - 1].State == BlockState.Empty || blockManager.Blocks[column, rows.Value - 1].State == BlockState.Falling) {
                    blockManager.Blocks[column, rows.Value].Faller.SetTarget(blockManager.Blocks[column, rows.Value - 1]);
                    blockManager.Blocks[column, rows.Value].Faller.ContinueFalling();
                }
            }
        }
    }
}