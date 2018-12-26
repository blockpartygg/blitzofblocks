using UnityEngine;
using System;

public class BlockRenderer_Idle : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] MeshRenderer meshRenderer = null;
    [SerializeField] BlockTypeColors typeColors = null;

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
        rootTransform.position = rootTransform.parent.position;

        if(block.State == BlockState.Idle) {
            meshRenderer.enabled = true;
        }
    }

    void UpdateRenderType() {
        if(block.Type != -1) {
            meshRenderer.material.color = typeColors.Colors[block.Type];
        }
        else {
            meshRenderer.material.color = Color.clear;
        }
    }
}