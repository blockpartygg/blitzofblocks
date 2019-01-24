using UnityEngine;

public class CursorController_Keyboard : MonoBehaviour {
    [SerializeField] CursorMover mover = null;
    [SerializeField] CursorSwapper swapper = null;
    [SerializeField] BoardRaiser raiser = null;

    void Update() {
        DetectMovement();
        DetectSwap();
        DetectRaise();
    }

    void DetectMovement() {
        Vector2 direction = Vector2.zero;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction.x = -1;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            direction.x = 1;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            direction.y = 1;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            direction.y = -1;
        }

        if(direction != Vector2.zero) {
            mover.Move(direction);
        }
    }

    void DetectSwap() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            swapper.Swap();
        }
    }

    void DetectRaise() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            raiser.ManuallyRaise();
        }
    }
}