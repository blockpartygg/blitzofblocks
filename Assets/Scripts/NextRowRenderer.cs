using UnityEngine;

public class NextRowRenderer : MonoBehaviour {
    [SerializeField] BoardRaiser raiser = null;
    [SerializeField] FloatReference raiseDuration = null;
    [SerializeField] Transform spriteTransform = null;

    void Update() {
        Vector3 raiseTranslation = new Vector3(0, raiser.Elapsed / raiseDuration.Value);
        spriteTransform.position = spriteTransform.parent.position + raiseTranslation;
    }
}