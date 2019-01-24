using UnityEngine;
using System;

public class BlockRenderer_Matched : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockRenderer blockRenderer = null;
    [SerializeField] BlockMatcher matcher = null;
    [SerializeField] Transform rootTransform = null;
    [SerializeField] FloatReference matchFlashDuration = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] BlockTypes types = null;

    void Start() {
        block.StateChanged += HandleStateChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateRenderState();
    }

    void UpdateRenderState() {
        if(block.State == BlockState.Matched) {
            rootTransform.position = rootTransform.parent.position + blockRenderer.RaiseTranslation;
        }
    }

    public void UpdateRenderer() {
        if(block.State == BlockState.Matched) {
            if(block.Type >= 0 && block.Type < types.Types.Count) {
                spriteRenderer.sprite = matcher.Elapsed % matchFlashDuration.Value < matchFlashDuration.Value / 2 ? types.Types[block.Type].MatchedSprite : types.Types[block.Type].Sprite;
            }
        }
    }
}