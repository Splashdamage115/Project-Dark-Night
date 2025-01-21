using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] AudioClips;
    private AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        int chosen = Random.Range(0, AudioClips.Length);
        AudioSource.clip = AudioClips[chosen];
        AudioSource.pitch = Random.Range(0.8f, 1.2f);
        AudioSource.Play();
    }
}
