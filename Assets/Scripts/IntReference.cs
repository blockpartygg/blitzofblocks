using UnityEngine;
using System;

[Serializable]
public class IntReference {
    [SerializeField] bool useConstant = false;
    [SerializeField] int constantValue = 0;
    [SerializeField] IntVariable variable = null;

    public int Value {
        get {
            return useConstant ? constantValue : variable.Value;
        }
    }
}