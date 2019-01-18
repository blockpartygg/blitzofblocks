using UnityEngine;
using System;

public class Cursor : MonoBehaviour {
    [SerializeField] int column, row;
    [SerializeField] Block selectedBlock;

    public int Column {
        get { return column; }
        set {
            column = value;
            CursorMoved?.Invoke(this, null);
        }
    }
    
    public int Row {
        get { return row; }
        set {
            row = value;
            CursorMoved?.Invoke(this, null);
        }
    }

    public Block SelectedBlock {
        get { return selectedBlock; }
        set {
            selectedBlock = value;
            SelectedBlockChanged?.Invoke(this, null);
        }
    }

    public event EventHandler CursorMoved;
    public event EventHandler SelectedBlockChanged;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;

    void Start() {
        Column = (int)Mathf.Floor((columns.Value - 1) / 2);
        Row = (int)Mathf.Floor((rows.Value - 1) / 2);
    }
}