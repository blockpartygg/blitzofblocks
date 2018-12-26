using UnityEngine;
using System;

[Serializable]
public class FloatReference {
    [SerializeField] bool useConstant = false;
    [SerializeField] float constantValue = 0;
    [SerializeField] FloatVariable variable = null;

    public float Value {
        get {
            return useConstant ? constantValue : variable.Value;
        }
    }
}