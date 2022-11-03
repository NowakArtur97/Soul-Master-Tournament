using UnityEngine;

public class Sounds : MonoBehaviour
{
    private const string ACTIVE_CLIP_TITLE = "_Active";
    private const string ACTIVED_CLIP_TITLE = "_Actived";

    private string _environmentHazardName;

    public void SetName(string environmentHazardName) => _environmentHazardName = environmentHazardName;

    public void PlayActiveSound() => AudioManager.Instance.Play(_environmentHazardName + ACTIVE_CLIP_TITLE);

    public void PlayActivedSound() => AudioManager.Instance.Play(_environmentHazardName + ACTIVED_CLIP_TITLE);
}
