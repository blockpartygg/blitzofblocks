using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public enum PanelType {
    Combo,
    Chain
}

public class Panel : MonoBehaviour {
	public int Column, Row;
    [SerializeField] Transform rootTransform = null;
	[SerializeField] MeshRenderer meshRenderer = null;
	[SerializeField] List<Material> materials = null;
	[SerializeField] TMP_Text text = null;

	void Start() {
		transform.position = new Vector3(Column, Row);
        meshRenderer.enabled = false;
		text.enabled = false;
	}

	public void Play(PanelType type, int value) {
		meshRenderer.enabled = true;
		meshRenderer.material = materials[(int)type];
		text.enabled = true;
		text.text = type == PanelType.Combo ? value.ToString() : "<size=6>x</size>" + value.ToString();
		rootTransform.DOMoveY(rootTransform.parent.position.y + 0.5f, 1f).OnComplete(() => { 
            rootTransform.position = rootTransform.parent.position;
            meshRenderer.enabled = false; 
            text.enabled = false; 
        });
	}
}