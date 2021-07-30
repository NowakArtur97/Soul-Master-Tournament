using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _sounds.ToList().ForEach(SetUpAudio);
        }
    }

    public void Play(string title)
    {
        Sound sound = _sounds.FirstOrDefault(s => s.title.Equals(title));

        if (sound == null)
        {
            Debug.LogWarning("Sound with title: " + title + " not found!");
        }
        else
        {
            sound.source.Play();
        }
    }

    public void Pause(string title)
    {
        Sound sound = _sounds.FirstOrDefault(s => s.title.Equals(title));

        if (sound == null)
        {
            Debug.LogWarning("Sound with title: " + title + " not found!");
        }
        else
        {
            sound.source.Pause();
            sound.source.loop = false;
        }
    }

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
