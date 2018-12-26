using UnityEngine;

public class BlockRenderer_Matched : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockMatcher matcher = null;
    [SerializeField] FloatReference matchFlashDuration = null;
    [SerializeField] ColorReference matchFlashColor = null;
    [SerializeField] MeshRenderer meshRenderer = null;
    [SerializeField] BlockTypeColors typeColors = null;

    void Update() {
        if(block.State == BlockState.Matched) {
            meshRenderer.material.color = matcher.Elapsed % matchFlashDuration.Value < matchFlashDuration.Value / 2 ? matchFlashColor.Value : typeColors.Colors[block.Type];
        }
    }
}