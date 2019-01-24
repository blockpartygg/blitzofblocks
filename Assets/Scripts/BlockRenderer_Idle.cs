using UnityEngine;
using System;

public class BlockRenderer_Idle : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockRenderer blockRenderer = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] BlockTypes types = null;

    void Start() {
        UpdateRenderState();
        UpdateRenderType();

        block.StateChanged += HandleStateChanged;
        block.TypeChanged += HandleTypeChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void HandleTypeChanged(object sender, EventArgs args) {
        UpdateRenderType();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.Idle) {
            spriteRenderer.enabled = true;
        }

        UpdateRenderer();
    }

    void UpdateRenderType() {
        if(block.Type != -1) {
            spriteRenderer.sprite = types.Types[block.Type].Sprite;
        }
        else {
            spriteRenderer.sprite = null;
        }
    }

    public void UpdateRenderer() {
        rootTransform.position = rootTransform.parent.position + blockRenderer.RaiseTranslation;
    }
}