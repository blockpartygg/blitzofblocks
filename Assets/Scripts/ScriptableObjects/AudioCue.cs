using UnityEngine;

[CreateAssetMenu]
public class AudioCue : ScriptableObject {
    [SerializeField] AudioClip clip;
    [Range(0, 1f)] public float Volume = 1;
    [Range(0.1f, 3f)] public float Pitch = 1;

    public void Play(AudioSource source) {
        if(clip == null) {
            return;
        }

        source.clip = clip;
        source.volume = Volume;
        source.pitch = Pitch;
        source.Play();
    }
}
