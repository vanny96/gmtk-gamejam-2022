using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClipData dieMovement;

    public void Play(Audio audio)
    {
        AudioClipData clipToPlay = FindClip(audio);
        StartCoroutine(PlayClip(clipToPlay));
    }

    private IEnumerator PlayClip(AudioClipData clipData)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = clipData.audioClip;
        audioSource.volume = clipData.volume;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
        
        Destroy(audioSource);
    }

    private AudioClipData FindClip(Audio audio)
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
    
    [Serializable]
    public struct AudioClipData
    {
        public AudioClip audioClip;
        [Range(0,1)]
        public float volume;
    }
}
