using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResolutionsDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _availableResolutions;

    private void Start() => SetupResolutionDropdownOptions();

    private void SetupResolutionDropdownOptions()
    {
        _availableResolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = _availableResolutions.Select(resolution => $"{resolution.width} x {resolution.height}").Distinct().ToList();

        int currentResolutionIndex = Array.FindIndex(_availableResolutions, resolution => HasSameResolution(resolution));

        _resolutionDropdown.AddOptions(resolutionOptions);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    private static bool HasSameResolution(Resolution resolution) => resolution.width == Screen.currentResolution.width
                && resolution.height == Screen.currentResolution.height;

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
}
