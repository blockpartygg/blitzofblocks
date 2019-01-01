using UnityEngine;
using System;

public class BlockRenderer_Empty : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;

    void Start() {
        UpdateRenderState();

        block.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.WaitingToEmpty || block.State == BlockState.Empty) {
            rootTransform.localScale = Vector3.one;
            spriteRenderer.enabled = false;
        }
    }
}