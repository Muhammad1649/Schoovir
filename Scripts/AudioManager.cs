using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
public class AudioManager : MonoBehaviour {

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixerGroup musicAudioMixer;
    [SerializeField] private AudioMixerGroup sfxAudioMixer;
    [SerializeField] private AudioMixerGroup assistantAudioMixer;

    [Header("Music Sounds")]
    [SerializeField] private Sounds[] music;
    [Header("SFX Sounds")]
    [SerializeField] private Sounds[] sFX;
    [Header("Assistant Sounds")]
    [SerializeField] private Sounds[] assistant;

    public static AudioManager instance;
    void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Load Sounds
        foreach (var sound in music) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = musicAudioMixer;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (var sound in sFX) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = sfxAudioMixer;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (var sound in assistant) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = assistantAudioMixer;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    // Play Sounds
    public void PlayMusic(string name) {
        Sounds s = Array.Find(music, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Play();
    }

    public void PlaySfx(string name) {
        Sounds s = Array.Find(sFX, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Play();
    }

    public void AssistantSpeek(string name) {
        Sounds s = Array.Find(assistant, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Play();
    }

    // Stop Sounds
    public void StopMusic(string name) {
        Sounds s = Array.Find(music, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Stop();
    }

    public void StopSfx(string name) {
        Sounds s = Array.Find(sFX, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Stop();
    }

    public void AssistantStop(string name) {
        Sounds s = Array.Find(assistant, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Could not find sound: " + name);
            return;
        }
        s.source.Stop();
    }
}
