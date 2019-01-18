using UnityEngine;
using System;

public class CursorRenderer : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] Transform keyboardRoot = null;
    [SerializeField] Transform mouseRoot = null;
    [SerializeField] SpriteRenderer keyboardSpriteRenderer = null;
    [SerializeField] SpriteRenderer mouseSpriteRenderer = null;
    public bool IsVisible = true;
    Vector3 cursorTranslation;

    void Start() {
        cursorTranslation = Vector3.zero;
        cursor.CursorMoved += HandleCursorMoved;
        cursor.SelectedBlockChanged += HandleSelectedBlockChanged;
    }

    void HandleCursorMoved(object sender, EventArgs args) {
        SetKeyboardVisible(true);
        SetMouseVisible(false);
    }

    void HandleSelectedBlockChanged(object sender, EventArgs args) {
        if(cursor.SelectedBlock != null) {
            SetMouseVisible(true);
        }
        else {
            SetMouseVisible(false);
        }
        
        SetKeyboardVisible(false);
    }

    public void SetKeyboardVisible(bool isVisible) {
        keyboardSpriteRenderer.enabled = isVisible;
    }

    public void SetMouseVisible(bool isVisible) {
        mouseSpriteRenderer.enabled = isVisible;
    }

    void Update() {
        cursorTranslation.Set(cursor.Column, cursor.Row, 0);
        keyboardRoot.position = keyboardRoot.parent.position + cursorTranslation;

        if (cursor.SelectedBlock != null) {
            mouseRoot.position = mouseRoot.parent.position + new Vector3(cursor.SelectedBlock.Column, cursor.SelectedBlock.Row, 0);
        }
    }
}