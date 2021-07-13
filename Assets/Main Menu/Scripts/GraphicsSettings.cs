using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);
}
