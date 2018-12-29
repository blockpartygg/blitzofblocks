using UnityEngine;

public class BlockChainer : MonoBehaviour {
    public bool JustEmptied;
    public bool ChainEligible { get; private set; }

    public void SetChainEligibility(bool eligibility) {
        ChainEligible = eligibility;
    }
}