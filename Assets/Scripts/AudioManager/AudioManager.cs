using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip dieMovement;

    public void Play(Audio audio)
    {
        AudioClip clipToPlay = FindClip(audio);
        StartCoroutine(PlayClip(clipToPlay));
    }

    private IEnumerator PlayClip(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
        
        Destroy(audioSource);
    }

    private AudioClip FindClip(Audio audio)
    {
        return audio switch
        {
            Audio.DieMovement => dieMovement,
            _ => throw new ArgumentOutOfRangeException(nameof(audio), audio, null)
        };
    }

    public enum Audio
    {
        DieMovement
    }
}
