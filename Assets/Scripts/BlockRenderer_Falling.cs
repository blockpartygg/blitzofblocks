using UnityEngine;
using System;

public class BlockRenderer_Falling : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockFaller faller = null;
    [SerializeField] FloatReference fallDuration = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] MeshRenderer meshRenderer = null;

    void Start() {
        UpdateRenderState();

        block.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.WaitingToFall || block.State == BlockState.Falling) {
            meshRenderer.enabled = true;
        }
    }

    void Update() {
        if(block.State == BlockState.Falling) {
            float timePercentage = faller.Elapsed / fallDuration.Value;
            Vector3 fallTranslation = new Vector3(0, -1 * timePercentage);
            rootTransform.position = rootTransform.parent.position + fallTranslation;
        }
    }
}