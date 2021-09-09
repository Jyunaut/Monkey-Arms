using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

    public void PlayRandomSound()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Count-1)]);
    }
}
