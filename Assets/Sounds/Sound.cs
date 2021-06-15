using System;
using UnityEngine;

[Serializable]
public class Sound
{
    [HideInInspector]
    public string title;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(0, 1)]
    public float pitch;

    public bool shouldLoop;

    [HideInInspector]
    public AudioSource source;
}
