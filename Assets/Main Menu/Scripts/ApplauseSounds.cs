using UnityEngine;

public class ApplauseSounds : MonoBehaviour
{
    [SerializeField]
    private string _applauseSound = "Applause";

    private void Start() => AudioManager.Instance.Play(_applauseSound);
}
