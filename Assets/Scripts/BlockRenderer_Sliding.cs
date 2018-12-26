using UnityEngine;
using System;

public class BlockRenderer_Sliding : MonoBehaviour {
    [SerializeField] Block block = null;
    [SerializeField] BlockSlider slider = null;
    [SerializeField] FloatReference slideDuration = null;
    [SerializeField] Transform rootTransform = null;

    void Update() {
        if(block.State == BlockState.Sliding) {
            float direction = slider.Direction == SlideDirection.Left ? -1 : 1;
            float timePercentage = slider.Elapsed / slideDuration.Value;
            Vector3 slideTranslation = new Vector3(direction * timePercentage, 0);
            rootTransform.position = rootTransform.parent.position + slideTranslation;
        }
    }
}