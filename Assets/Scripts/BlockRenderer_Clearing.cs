using UnityEngine;
using System;

public class BlockRenderer_Clearing : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockClearer clearer = null;
    [SerializeField] FloatReference clearDuration = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] MeshRenderer meshRenderer = null;
    [SerializeField] BlockTypeColors typeColors = null;
    [SerializeField] ParticleSystem particles = null;

    void Start() {
        block.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.WaitingToClear) {
            meshRenderer.material.color = typeColors.Colors[block.Type];
        }

        if(block.State == BlockState.Clearing) {
            var mainModule = particles.main;
            mainModule.startColor = typeColors.Colors[block.Type];
            particles.Play();
        }
    }

    void Update() {
        if(block.State == BlockState.Clearing) {
            float timePercentage = clearer.Elapsed / clearDuration.Value;
            rootTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timePercentage);
        }
    }
}