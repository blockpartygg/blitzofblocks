using UnityEngine;
using System.Collections.Generic;

public class MatchDetector : MonoBehaviour {
    Queue<Block> matchDetectionRequests;
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] ScoreManager scoreManager = null;
    // [SerializeField] PanelManager panelManager; // TODO: Convert this to an event to reduce coupling
    // [SerializeField] ChainDetector chainDetector = null; // TODO: Implement ChainDetector
    // public AudioSource AudioSource; // TODO: Convert this to an event
    // public AudioClip BonusClip; // TODO: Convert this to an event
    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference minimumMatchLength = null;

    void Awake() {
        matchDetectionRequests = new Queue<Block>();
    }

    public void RequestMatchDetection(Block block) {
        matchDetectionRequests.Enqueue(block);
    }

    void Update() {
        while(matchDetectionRequests.Count > 0) {
            Block request = matchDetectionRequests.Dequeue();
            DetectMatch(request);
        }
    }

    void DetectMatch(Block block) {
        if(block.State != BlockState.Idle)
        {
            return;
        }

        int leftColumn = block.Column;
        while(leftColumn > 0 && blockManager.Blocks[leftColumn - 1, block.Row].State == BlockState.Idle && blockManager.Blocks[leftColumn - 1, block.Row].Type == block.Type) {
            leftColumn--;
        }

        int rightColumn = block.Column + 1;
        while(rightColumn < boardColumns.Value && blockManager.Blocks[rightColumn, block.Row].State == BlockState.Idle && blockManager.Blocks[rightColumn, block.Row].Type == block.Type) {
            rightColumn++;
        }

        int bottomRow = block.Row;
        while(bottomRow > 0 && blockManager.Blocks[block.Column, bottomRow - 1].State == BlockState.Idle && blockManager.Blocks[block.Column, bottomRow - 1].Type == block.Type) {
            bottomRow--;
        }

        int topRow = block.Row + 1;
        while(topRow < boardColumns.Value && blockManager.Blocks[block.Column, topRow].State == BlockState.Idle && blockManager.Blocks[block.Column, topRow].Type == block.Type) {
            topRow++;
        }

        int width = rightColumn - leftColumn;
        int height = topRow - bottomRow;
        int matchedBlockCount = 0;
        bool horizontalMatch = false;
        bool verticalMatch = false;

        if(width >= minimumMatchLength.Value) {
            horizontalMatch = true;
            matchedBlockCount += width;
        }

        if(height >= minimumMatchLength.Value) {
            verticalMatch = true;
            matchedBlockCount += height;
        }

        if(!horizontalMatch && !verticalMatch) {
            // block.Chainer.ChainEligible = false; TODO: Make this a EndChainInvolvement method in Chainer
            return;
        }

        // If there's a horizontal and vertical match, remove the common block
        if(horizontalMatch && verticalMatch) {
            matchedBlockCount--;
        }

        int delayCounter = matchedBlockCount;
        bool incrementChain = false;

        if(horizontalMatch) {
            for(int matchColumn = leftColumn; matchColumn < rightColumn; matchColumn++) {
                blockManager.Blocks[matchColumn, block.Row].Matcher.Match(matchedBlockCount, delayCounter--);
                //if(blockManager.Blocks[matchColumn, block.Row].Chainer.ChainEligible) {
                    // ChainDetector.AddChainContributingBlock(BlockManager.Blocks[matchColumn, block.Row]); // TODO: Implement ChainDetector
                //    incrementChain = true;
                //}
            }
        }

        if(verticalMatch) {
            for(int matchRow = topRow - 1; matchRow >= bottomRow; matchRow--) {
                blockManager.Blocks[block.Column, matchRow].Matcher.Match(matchedBlockCount, delayCounter--);
                // if(blockManager.Blocks[block.Column, matchRow].Chainer.ChainEligible) {
                    // ChainDetector.AddChainContributingBlock(BlockManager.Blocks[block.Column, matchRow]); // TODO: Implement ChainDetector
                //    incrementChain = true;
                //}
            }
        }

        // block.Chainer.ChainEligible = false; // TODO: Implement Chainer

        bool playSound = false;

        // AudioSource.pitch = 1f; // Implement Audio

        if(matchedBlockCount > minimumMatchLength.Value) {
            scoreManager.ScoreCombo(matchedBlockCount);
            // PanelManager.Panels[block.Column, block.Row].Play(PanelType.Combo, matchedBlockCount); TODO: Implement PanelManager
            playSound = true;
        }

        if(incrementChain) {
            // ChainDetector.IncrementChain(); // TODO: Implement ChainDetector
            int row = matchedBlockCount > minimumMatchLength.Value ? block.Row + 1 : block.Row;
            //if(row <= PanelManager.Rows - 1) {
                // PanelManager.Panels[block.Column, row].Play(PanelType.Chain, ChainDetector.ChainLength); // TODO: Implement PanelManager
                // AudioSource.pitch = Mathf.Min(0.75f + ChainDetector.ChainLength * 0.25f, 2f); // TODO: Implement Audio
            //}
            playSound = true;
        }

        if(playSound) {
            // AudioSource.clip = BonusClip; // TODO: Implement Audio
            // AudioSource.Play();
        }
    }
}