using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
}