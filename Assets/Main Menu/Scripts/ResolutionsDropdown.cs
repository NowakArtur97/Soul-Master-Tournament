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

    private void Start()
    {
        SetupResolutionDropdownOptions();
        ChoseDefaultOptions();
    }

    private void SetupResolutionDropdownOptions()
    {
        _availableResolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        _resolutionDropdown.AddOptions(
            _availableResolutions.Select(resolution => $"{resolution.width} x {resolution.height}")
            .Distinct()
            .ToList());
    }

    private void ChoseDefaultOptions()
    {
        int currentResolutionIndex = Array.FindIndex(_availableResolutions, resolution => HasSameResolution(resolution));
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        SetResolution(currentResolutionIndex);
        SetFullscreen(true);
    }

    private bool HasSameResolution(Resolution resolution) => resolution.width == Screen.currentResolution.width
                && resolution.height == Screen.currentResolution.height;

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
}
