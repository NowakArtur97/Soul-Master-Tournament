using UnityEngine;

public class GraphicsDropdown : MonoBehaviour
{
    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);
}
