using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffect : MonoBehaviour {
    [Header("Effect Settings")]
    [SerializeField] private string collisionSoundName;
    [SerializeField] private LayerMask interactionLayers;
    [SerializeField] private AudioMixerGroup audioMixer;

    [Space]
    [SerializeField] private bool useVelocity = true;
    [SerializeField] private float velocityRange;

    [Space]
    [SerializeField] private bool randomizePitch = true;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.2f;

    [Header("Sound Effects")]
    [SerializeField] private Sounds[] sounds;

    private float velocity;
    

    private void Awake() {
        foreach (Sounds sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = audioMixer;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.spatialize = true;
            sound.source.spatialBlend = 1;
            sound.source.maxDistance = 4;
            sound.source.rolloffMode = AudioRolloffMode.Logarithmic;
        }
    }

    private void OnCollisionEnter(Collision collider) {
        if (interactionLayers.value == (interactionLayers | (1 << collider.gameObject.layer))) {
            if (collisionSoundName != "") {
                velocity = collider.relativeVelocity.magnitude;
                Play(collisionSoundName);
            }
        }
    }

    public void Play(string name) {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        }

        if (useVelocity) {
            s.source.volume = (velocity + velocityRange) / 10;

            if (randomizePitch) {
                s.source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            }
        }

        s.source.Play();
    }
}
