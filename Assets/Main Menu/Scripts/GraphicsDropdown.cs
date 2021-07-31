using TMPro;
using UnityEngine;

public class GraphicsDropdown : MonoBehaviour
{
    private const int MAXIMUM_QUALITY_LEVEL = 2;

    [SerializeField]
    private TMP_Dropdown _graphicsDropdown;

    private void Start() => ChoseDefaultOptions();

    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    private void ChoseDefaultOptions()
    {
        QualitySettings.SetQualityLevel(MAXIMUM_QUALITY_LEVEL);

        _graphicsDropdown.value = MAXIMUM_QUALITY_LEVEL;
        _graphicsDropdown.RefreshShownValue();
    }
}
