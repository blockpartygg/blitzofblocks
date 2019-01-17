using UnityEngine;

public class CursorSwapper : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] CursorSelector selector = null;
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference columns = null;
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
        Block leftBlock = blockManager.Blocks[cursor.Column, cursor.Row];
        Block rightBlock = blockManager.Blocks[cursor.Column + 1, cursor.Row];

        SwapBlocks(leftBlock, rightBlock);
    }

    public void SwapLeft() {
        if (cursor.SelectedBlock != null && cursor.SelectedBlock.State == BlockState.Idle && cursor.SelectedBlock.Column - 1 >= 0) {
            Block leftBlock = blockManager.Blocks[cursor.SelectedBlock.Column - 1, cursor.SelectedBlock.Row];
            Block rightBlock = blockManager.Blocks[cursor.SelectedBlock.Column, cursor.SelectedBlock.Row];
            selector.Select(leftBlock);

            SwapBlocks(leftBlock, rightBlock);
        }
    }

    public void SwapRight() {
        if (cursor.SelectedBlock != null && cursor.SelectedBlock.State == BlockState.Idle && cursor.SelectedBlock.Column + 1 < columns.Value) {
            Block leftBlock = blockManager.Blocks[cursor.SelectedBlock.Column, cursor.SelectedBlock.Row];
            Block rightBlock = blockManager.Blocks[cursor.SelectedBlock.Column + 1, cursor.SelectedBlock.Row];
            selector.Select(rightBlock);

            SwapBlocks(leftBlock, rightBlock);
        }
    }

    bool SwapBlocks(Block leftBlock, Block rightBlock) {
        if (IsActive && IsSwappable(leftBlock) && IsSwappable(rightBlock)) {
            leftBlock.Slider.SetupSlide(rightBlock);
            rightBlock.Slider.SetupSlide(leftBlock);
            leftBlock.Slider.Slide(SlideDirection.Right);
            rightBlock.Slider.Slide(SlideDirection.Left);
            cue.Play(source);
            return true;
        }

        return false;
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