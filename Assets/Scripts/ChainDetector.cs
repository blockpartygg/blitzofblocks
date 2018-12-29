using UnityEngine;
using System.Collections.Generic;

public class ChainDetector : MonoBehaviour {
	[SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;
	[SerializeField] ScoreManager scoreManager = null;
	public int ChainLength { get; private set; }
	List<Block> chainContributingBlocks;

	void Awake() {
		chainContributingBlocks = new List<Block>();
	}

    void Start() {
        ChainLength = 1;
    }

	public void AddChainContributingBlock(Block block) {
		chainContributingBlocks.Add(block);
	}

	public void IncrementChain() {
		ChainLength++;
		scoreManager.ScoreChain(ChainLength);
	}

	void Update() {
		for(int column = 0; column < boardColumns.Value; column++) {
			for(int row = 0; row < boardRows.Value; row++) {
				if(blockManager.Blocks[column, row].Chainer.JustEmptied) {
					for(int chainEligibleRow = row + 1; chainEligibleRow < boardRows.Value; chainEligibleRow++) {
						if(blockManager.Blocks[column, chainEligibleRow].State == BlockState.Idle) {
							blockManager.Blocks[column, chainEligibleRow].Chainer.SetChainEligibility(true);
						}
					}
				}

				blockManager.Blocks[column, row].Chainer.JustEmptied = false;
			}
		}

        bool stopChain = true;

		for(int column = 0; column < boardColumns.Value; column++) {
			for(int row = 0; row < boardRows.Value; row++) {
				if(blockManager.Blocks[column, row].Chainer.ChainEligible) {
					stopChain = false;
				}
			}
		}

		for(int index = chainContributingBlocks.Count - 1; index >= 0; index--) {
			if(chainContributingBlocks[index].State == BlockState.Matched || chainContributingBlocks[index].State == BlockState.WaitingToClear || chainContributingBlocks[index].State == BlockState.Clearing || chainContributingBlocks[index].State == BlockState.WaitingToEmpty) {
				stopChain = false;
			}
			else {
				chainContributingBlocks.Remove(chainContributingBlocks[index]);
			}
		}

		if(ChainLength > 1 && stopChain) {
			if(ChainLength > 1) {
				// Todo: play fanfare
			}

			ChainLength = 1;
		}
	}
}