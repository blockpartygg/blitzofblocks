using UnityEngine;

public enum SlideDirection {
    None,
    Left,
    Right,
}

public class BlockSlider : MonoBehaviour {   
    [SerializeField] Block block = null;
    [SerializeField] FloatReference slideDuration = null;
    public SlideDirection Direction;
    BlockState targetState;
    int targetType;
    public float Elapsed;
    MatchDetector matchDetector;

    void Awake() {
        matchDetector = (MatchDetector)FindObjectOfType(typeof(MatchDetector));
    }

    public void SetupSlide(Block target) {
        targetState = target.State;
        targetType = target.Type;
    }

    public void Slide(SlideDirection direction) {
        block.State = BlockState.Sliding;
        Elapsed = 0f;
        Direction = direction;
    }

    void Update() {
        if(block.State == BlockState.Sliding) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= slideDuration.Value) {
                block.State = targetState;
                block.Type = targetType;
                Elapsed = 0;
                Direction = SlideDirection.None;

                if(matchDetector != null) {
                    matchDetector.RequestMatchDetection(block);
                }
            }
        }
    }
}