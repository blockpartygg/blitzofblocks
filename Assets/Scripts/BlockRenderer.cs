using UnityEngine;

public class BlockRenderer : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockRenderer_Clearing clearing = null;
    [SerializeField] BlockRenderer_Falling falling = null;
    [SerializeField] BlockRenderer_Matched matched = null;
    [SerializeField] BlockRenderer_Sliding sliding = null;

    void Start() {
        transform.position = new Vector3(block.Column, block.Row);
    }

    void Update() {
        switch(block.State) {
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