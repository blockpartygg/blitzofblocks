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
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] List<Sprite> sprites = null;
	[SerializeField] TMP_Text text = null;
    [SerializeField] FloatReference boardRaiseDuration = null;
    Vector3 raiseTranslation;
    BoardRaiser raiser;

    void Awake() {
        GameObject blocks = GameObject.Find("Blocks");
        raiser = blocks.GetComponent<BoardRaiser>();
    }

    void Start() {
		transform.position = new Vector3(Column, Row);
        spriteRenderer.enabled = false;
		text.enabled = false;
	}

	public void Play(PanelType type, int value) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprites[(int)type];
		text.enabled = true;
		text.text = type == PanelType.Combo ? value.ToString() : "<size=6>x</size>" + value.ToString();
        rootTransform.position = rootTransform.parent.position + raiseTranslation;
		rootTransform.DOMoveY(rootTransform.parent.position.y + raiseTranslation.y + 0.5f, 1f).OnComplete(() => { 
            rootTransform.position = rootTransform.parent.position + raiseTranslation;
            spriteRenderer.enabled = false;
            text.enabled = false; 
        });
	}

    void Update() {
        raiseTranslation = Vector3.zero;
        if (raiser != null) {
            raiseTranslation = new Vector3(0, raiser.Elapsed / boardRaiseDuration.Value);
        }
    }
}