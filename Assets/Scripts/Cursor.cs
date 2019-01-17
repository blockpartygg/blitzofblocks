using UnityEngine;
using System;

public class Cursor : MonoBehaviour {
    public int Column, Row;

    Block selectedBlock;
    public Block SelectedBlock {
        get { return selectedBlock; }
        set {
            selectedBlock = value;
            SelectedBlockChanged?.Invoke(this, null);
        }
    }

    public event EventHandler SelectedBlockChanged;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;

    void Start() {
        Column = (int)Mathf.Floor((columns.Value - 1) / 2);
        Row = (int)Mathf.Floor((rows.Value - 1) / 2);
    }
}