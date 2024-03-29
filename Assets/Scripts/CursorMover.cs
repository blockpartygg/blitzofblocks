using UnityEngine;

public class CursorMover : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;
    [SerializeField] AudioCue audioCue = null;
    [SerializeField] AudioSource audioSource = null;

    public void Move(Vector2 direction) {
        bool playSound = false;

        // Since the cursor is 2 blocks wide, constrain position from 0 to Board.Columns - 2
        if(cursor.Column + direction.x >= 0 && cursor.Column + direction.x <= columns.Value - 2) {
            cursor.Column += (int)direction.x;
            playSound = true;
        }
        
        // Since the board has an invisible top row at y = rows, constrain position from 0 to rows - 1
        if(cursor.Row + direction.y >= 0 && cursor.Row + direction.y <= rows.Value - 1) {
            cursor.Row += (int)direction.y;
            playSound = true;
        }

        if(playSound) {
            audioCue.Play(audioSource);
        }
    }

    public void MoveTo(int column, int row) {
        
    }
}