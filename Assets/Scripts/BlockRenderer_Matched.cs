using UnityEngine;

public class BlockRenderer_Matched : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockMatcher matcher = null;
    [SerializeField] FloatReference matchFlashDuration = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] BlockTypes types = null;

    public void UpdateRenderer() {
        if(block.State == BlockState.Matched) {
            if(block.Type >= 0 && block.Type < types.Types.Count) {
                spriteRenderer.sprite = matcher.Elapsed % matchFlashDuration.Value < matchFlashDuration.Value / 2 ? types.Types[block.Type].MatchedSprite : types.Types[block.Type].Sprite;
            }
        }
    }
}