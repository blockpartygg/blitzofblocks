using UnityEngine;

public class CursorSwapper : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] BlockManager blockManager = null;
    [SerializeField] IntReference rows = null;

    public void Swap() {
        Block leftBlock = blockManager.Blocks[cursor.Column, cursor.Row];
        Block rightBlock = blockManager.Blocks[cursor.Column + 1, cursor.Row];

        if(IsSwappable(leftBlock) && IsSwappable(rightBlock)) {
            leftBlock.Slider.SetupSlide(rightBlock);
            rightBlock.Slider.SetupSlide(leftBlock);
            leftBlock.Slider.Slide(SlideDirection.Right);
            rightBlock.Slider.Slide(SlideDirection.Left);
            // TODO: Play the swap sound
            // AudioSource.clip = SlideClip;
            // AudioSource.pitch = 1f;
            // AudioSource.Play();
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