﻿using UnityEngine;

public class BlockManager_TimeAttackCreator : MonoBehaviour {
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;

    void Start() {
        for (int row = 0; row < boardRows.Value + 1; row++) {
            for (int column = 0; column < boardColumns.Value; column++) {
                blockManager.Blocks[column, row].State = BlockState.Idle;
                blockManager.Blocks[column, row].Type = blockManager.GetRandomBlockType(column, row);
            }
        }
    }
}