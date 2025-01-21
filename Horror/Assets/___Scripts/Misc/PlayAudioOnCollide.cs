using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioPlayer>().playAudio();
    }
}
