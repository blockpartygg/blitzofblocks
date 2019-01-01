using UnityEngine;
using System;

public class BlockRenderer_Clearing : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockClearer clearer = null;
    [SerializeField] FloatReference clearDuration = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] BlockTypes types = null;
    [SerializeField] ParticleSystem particles = null;

    void Start() {
        block.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.WaitingToClear) {
            if(block.Type >= 0 && block.Type < types.Types.Count) {
                spriteRenderer.sprite = types.Types[block.Type].ClearingSprite;
            }
        }

        if(block.State == BlockState.Clearing) {
            if(block.Type >= 0 && block.Type < types.Types.Count) {
                var mainModule = particles.main;
                mainModule.startColor = types.Types[block.Type].Color;
            }
            particles.Play();
        }
    }

    public void UpdateRenderer() {
        if(block.State == BlockState.Clearing) {
            float timePercentage = clearer.Elapsed / clearDuration.Value;
            rootTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timePercentage);
        }
    }
}