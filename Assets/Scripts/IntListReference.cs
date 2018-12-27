using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class IntListReference {
    [SerializeField] bool useConstant = false;
    [SerializeField] List<int> constantValue = null;
    [SerializeField] IntListVariable variable = null;

    public List<int> Value {
        get {
            return useConstant ? constantValue : variable.Value;
        }
    }
}