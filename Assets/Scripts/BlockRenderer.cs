using UnityEngine;

public class BlockRenderer : MonoBehaviour {
    [SerializeField] Block block = null;

    void Start() {
        transform.position = new Vector3(block.Column, block.Row);
    }
}