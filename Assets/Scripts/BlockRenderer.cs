using UnityEngine;

public class BlockRenderer : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockRenderer_Idle idle = null;
    [SerializeField] BlockRenderer_Sliding sliding = null;
    [SerializeField] BlockRenderer_Falling falling = null;
    [SerializeField] BlockRenderer_Matched matched = null;
    [SerializeField] BlockRenderer_Clearing clearing = null;
    [SerializeField] FloatReference boardRaiseDuration = null;
    public Vector3 RaiseTranslation;
    BoardRaiser raiser;

    void Awake() {
        GameObject blocks = GameObject.Find("Blocks");
        raiser = blocks.GetComponent<BoardRaiser>();
    }

    void Start() {
        transform.position = new Vector3(block.Column, block.Row);
    }

    void Update() {
        RaiseTranslation = Vector3.zero;
        if(raiser != null) {
            RaiseTranslation = new Vector3(0, raiser.Elapsed / boardRaiseDuration.Value);
        }

        switch(block.State) {
            case BlockState.Idle:
                idle.UpdateRenderer();
                break;
            case BlockState.Sliding:
                sliding.UpdateRenderer();
                break;
            case BlockState.Falling:
                falling.UpdateRenderer();
                break;
            case BlockState.Matched:
                matched.UpdateRenderer();
                break;
            case BlockState.Clearing:
                clearing.UpdateRenderer();
                break;
        }
    }
}