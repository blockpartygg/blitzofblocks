using UnityEngine;
using System;

[Serializable]
public class StringReference {
    [SerializeField] bool useConstant = false;
    [SerializeField] string constantValue = "";
    [SerializeField] StringVariable variable = null;

    public string Value {
        get {
            return useConstant ? constantValue : variable.Value;
        }
    }
}