using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        _sounds.ToList().ForEach(SetUpAudio);

        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void Play(string title) => _sounds.FirstOrDefault(sound => sound.title.Equals(title))?.source.Play();

    private void SetUpAudio(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();

        sound.title = sound.clip.name;
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.shouldLoop;
    }
}
