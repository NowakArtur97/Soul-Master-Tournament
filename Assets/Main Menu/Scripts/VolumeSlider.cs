using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _volumeSlider;

    [SerializeField]
    private AudioMixer _audioMixer;

    private void Start() => SetMaxVolume();

    private void SetMaxVolume()
    {
        _volumeSlider.value = _volumeSlider.maxValue;
        SetVolume(_volumeSlider.value);
    }

    public void SetVolume(float volume) => _audioMixer.SetFloat("volume", volume);
}
