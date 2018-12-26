using UnityEngine;
using System;

[Serializable]
public class ColorReference {
    [SerializeField] bool useConstant = false;
    [SerializeField] Color constantValue = Color.white;
    [SerializeField] ColorVariable variable = null;

    public Color Value {
        get {
            return useConstant ? constantValue : variable.Value;
        }
    }
}