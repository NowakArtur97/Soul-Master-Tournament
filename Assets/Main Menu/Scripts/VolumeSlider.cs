using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private int _startingVolume = 8;
    [SerializeField]
    private Slider _volumeSlider;
    [SerializeField]
    private AudioMixer _audioMixer;

    private void Start() => SetStartingVolume();

    private void SetStartingVolume()
    {
        _volumeSlider.value = _startingVolume;
        SetVolume(_volumeSlider.value);
    }

    public void SetVolume(float volume) => _audioMixer.SetFloat("volume", volume);
}
