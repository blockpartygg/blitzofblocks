using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class ScoreManager : ScriptableObject {
    [SerializeField] int points = 0;
    [SerializeField] GameEvent pointsChanged = null;
    public int Points {
        get { return points; }
        set {
            if(points != value) {
                points = value;
                pointsChanged.Raise();
            }
        }
    }

    [SerializeField] int blocksMatched = 0;
    [SerializeField] GameEvent blocksMatchedChanged = null;
    public int BlocksMatched {
        get { return blocksMatched; }
        set {
            if(blocksMatched != value) {
                blocksMatched = value;
                blocksMatchedChanged.Raise();
            }
        }
    }

    [SerializeField] Dictionary<int, int> comboCounts = new Dictionary<int, int>();
    [SerializeField] GameEvent comboCountsChanged = null;
    public Dictionary<int, int> ComboCounts {
        get { return comboCounts; }
    }

    [SerializeField] Dictionary<int, int> chainLengths = new Dictionary<int, int>();
    public GameEvent chainLengthsChanged = null;
    public Dictionary<int, int> ChainLengths {
        get { return chainLengths; }
    }

    [SerializeField] int linesRaised = 0;
    [SerializeField] GameEvent linesRaisedChanged = null;
    public int LinesRaised {
        get { return linesRaised; }
        set {
            if(linesRaised != value) {
                linesRaised = value;
                linesRaisedChanged.Raise();
            }
        }
    }

    [SerializeField] IntReference boardColumns = null;
    [SerializeField] IntReference boardRows = null;
    [SerializeField] IntReference matchValue = null;
    [SerializeField] IntListReference comboValues = null;
    [SerializeField] IntListReference chainValues = null;
    [SerializeField] IntReference raiseValue = null;

    void OnEnable() {
        Reset();
    }

    public void Reset() {
        Points = 0;
        BlocksMatched = 0;
        
        ComboCounts.Clear();
        for(int count = 0; count < boardColumns.Value * boardRows.Value; count++) {
            ComboCounts[count] = 0;
        }
        comboCountsChanged.Raise();

        ChainLengths.Clear();
        for(int length = 0; length < 256; length++) {
            ChainLengths[length] = 0;
        }
        chainLengthsChanged.Raise();

        LinesRaised = 0;
    }

    public void ScoreMatch() {
        int points = matchValue.Value;
        Points += points;
        BlocksMatched++;
    }

    public void ScoreCombo(int matchedBlockCount) {
        int points = comboValues.Value[Math.Min(matchedBlockCount - 1, comboValues.Value.Count - 1)];
        Points += points;
        ComboCounts[matchedBlockCount]++;
        comboCountsChanged.Raise();
    }

    public void ScoreChain(int chainLength) {
        int points = chainValues.Value[Math.Min(chainLength - 1, chainValues.Value.Count - 1)];
        Points += points;
        chainLengths[chainLength]++;
        chainLengthsChanged.Raise();
    }

    public void ScoreRaise() {
        int points = raiseValue.Value;
        Points += points;
        LinesRaised++;
    }
}