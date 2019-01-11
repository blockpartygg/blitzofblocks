using UnityEngine;

public class CursorSwapper : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference rows = null;
    [SerializeField] AudioCue cue = null;
    [SerializeField] AudioSource source = null;
    [SerializeField] bool isActive;
    public bool IsActive {
        get { return isActive; }
    }

    public void SetActive(bool isActive) {
        this.isActive = isActive;
    }

    public void Swap() {
        if(isActive) {
            Block leftBlock = blockManager.Blocks[cursor.Column, cursor.Row];
            Block rightBlock = blockManager.Blocks[cursor.Column + 1, cursor.Row];

            if(IsSwappable(leftBlock) && IsSwappable(rightBlock)) {
                leftBlock.Slider.SetupSlide(rightBlock);
                rightBlock.Slider.SetupSlide(leftBlock);
                leftBlock.Slider.Slide(SlideDirection.Right);
                rightBlock.Slider.Slide(SlideDirection.Left);
                cue.Play(source);
            }    
        }
    }

    bool IsSwappable(Block block) {
        bool isSwappable = (block.State == BlockState.Idle ||
            block.State == BlockState.Empty) &&
            (block.Row + 1 == rows.Value || block.Row + 1 < rows.Value &&
            blockManager.Blocks[block.Column, block.Row + 1].State != BlockState.Falling &&
            blockManager.Blocks[block.Column, block.Row + 1].State != BlockState.WaitingToFall);

        return isSwappable;
    }
}