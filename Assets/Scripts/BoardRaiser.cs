using UnityEngine;

public class BoardRaiser : MonoBehaviour {
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] BlockManager_NewRow newRow = null;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;
    [SerializeField] FloatReference raiseDuration = null;
    [SerializeField] FloatReference lossCountdownDuration = null;
    [SerializeField] FloatReference minimumRaiseRate = null;
    [SerializeField] FloatReference maximumRaiseRate = null;
    [SerializeField] FloatReference manualRaiseRate = null;
    [SerializeField] ScoreManager scoreManager = null;
    [SerializeField] MatchDetector matchDetector = null;
    [SerializeField] Cursor cursor = null;
    public float Elapsed;
    public float LossElapsed;
    float raiseRate;
    bool isManuallyRaising;
    bool isLossIncoming;

    public void ManuallyRaise() {
        isManuallyRaising = true;
    }

    void Update() {
        SetRaiseRate();
        DetectIncomingLoss();

        if(isLossIncoming) {
            LossElapsed += Time.deltaTime * raiseRate;

            if(LossElapsed >= lossCountdownDuration.Value) {
                Debug.Log("GAME OVER DUDE");
            }
        }
        else {
            Elapsed += Time.deltaTime * raiseRate;

            if(Elapsed >= raiseDuration.Value) {
                Elapsed = 0;
                RaiseBlocks();
                RaiseCursors();

                if (isManuallyRaising) {
                    scoreManager.ScoreRaise();
                    isManuallyRaising = false;                    
                }
            }

            LossElapsed = 0;
        }

        isLossIncoming = false;
    }

    void SetRaiseRate() {
        for (int column = 0; column < boardColumns.Value; column++) {
            for (int row = 0; row < boardRows.Value; row++) {
                if (blockManager.Blocks[column, row].State != BlockState.Empty &&
                    blockManager.Blocks[column, row].State != BlockState.Idle &&
                    blockManager.Blocks[column, row].State != BlockState.Sliding) {
                    raiseRate = 0;
                    return;
                }
            }
        }

        if (isManuallyRaising) {
            raiseRate = manualRaiseRate.Value;
        }
        else {
            raiseRate = minimumRaiseRate.Value; // TODO: Scale raise rate based on clock elapsed time
        }
    }

    void DetectIncomingLoss() {
        for (int column = 0; column < boardColumns.Value; column++) {
            if (blockManager.Blocks[column, boardRows.Value - 1].State != BlockState.Empty) {
                isLossIncoming = true;
            }
        }
    }

    void RaiseBlocks() {
        for (int column = 0; column < boardColumns.Value; column++) {
            for (int row = boardRows.Value - 1; row >= 1; row--) {
                blockManager.Blocks[column, row].State = blockManager.Blocks[column, row - 1].State;
                blockManager.Blocks[column, row].Type = blockManager.Blocks[column, row - 1].Type;
            }

            blockManager.Blocks[column, 0].State = newRow.Blocks[column].State;
            blockManager.Blocks[column, 0].Type = newRow.Blocks[column].Type;

            matchDetector.RequestMatchDetection(blockManager.Blocks[column, 0]);
        }

        newRow.CreateNewRowBlocks();
    }

    void RaiseCursors() {
        if (cursor.Row < boardRows.Value - 2) {
            cursor.Row++;
        }

        if (cursor.SelectedBlock != null && cursor.SelectedBlock.Row < boardRows.Value - 2) {
            cursor.SelectedBlock = blockManager.Blocks[cursor.SelectedBlock.Column, cursor.SelectedBlock.Row + 1];
        }
    }
}