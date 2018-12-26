using UnityEngine;

public class BlockTest : MonoBehaviour {
    [SerializeField] Block block = null;

    public void Empty() {
        block.Emptier.DelayDuration = 0;
        block.Emptier.Empty();
    }

    public void Idle() {
        block.State = BlockState.Idle;
    }

    public void SetType(int type) {
        block.Type = type;
    }

    public void Slide(bool slideLeft) {
        block.Slider.SetupSlide(block);
        block.Slider.Slide(slideLeft ? SlideDirection.Left : SlideDirection.Right);
    }

    public void Match() {
        block.Matcher.Match(1, 1);
    }
}