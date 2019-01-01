using UnityEngine;
using System;

public class CursorRenderer : MonoBehaviour {
    [SerializeField] Cursor cursor = null;
    [SerializeField] Transform Root = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    Vector3 cursorTranslation;

    void Start() {
        cursorTranslation = Vector3.zero;
    }

    public void SetVisible(bool isVisible) {
        spriteRenderer.enabled = isVisible;
    }

    void Update() {
        cursorTranslation.Set(cursor.Column, cursor.Row, 0);
        Root.position = cursorTranslation;
    }
}