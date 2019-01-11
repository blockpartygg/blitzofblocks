using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioCue))]
public class AudioEventEditor : Editor {
    [SerializeField] AudioSource previewSource;

    void OnEnable() {
        previewSource = EditorUtility.CreateGameObjectWithHideFlags("Preview Audio Cue", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    void OnDisable() {
        DestroyImmediate(previewSource.gameObject);    
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

        if(GUILayout.Button("Preview")) {
            ((AudioCue)target).Play(previewSource);
        }

        EditorGUI.EndDisabledGroup();
    }
}
