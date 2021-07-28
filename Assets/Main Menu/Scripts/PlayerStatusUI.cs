using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    private const string SOUL_IMAGE_GO_NAME = "Current Soul Image";
    private const string NUMBER_OF_LIVES_GO_NAME = "Number of lives";
    private const string NUMBER_OF_SOULS_GO_NAME = "Number of souls";

    [SerializeField]
    private Sprite _fireSoulImage;
    [SerializeField]
    private Sprite _waterSoulImage;
    [SerializeField]
    private Sprite _poisonousSoulImage;
    [SerializeField]
    private Sprite _evilEyeSoulImage;
    [SerializeField]
    private Sprite _iceSoulImage;

    private Image _soulImage;
    private TextMeshProUGUI _numberOfHearts;
    private TextMeshProUGUI _numberOfSouls;

    private Player _player;
    private Dictionary<string, Sprite> _soulsSprites;

    private void Awake()
    {
        _numberOfHearts = transform.Find(NUMBER_OF_LIVES_GO_NAME).GetComponent<TextMeshProUGUI>();
        _numberOfSouls = transform.Find(NUMBER_OF_SOULS_GO_NAME).GetComponent<TextMeshProUGUI>();
        _soulImage = transform.Find(SOUL_IMAGE_GO_NAME).GetComponent<Image>();

        _soulsSprites = new Dictionary<string, Sprite>()
        {
            { "Fire Soul", _fireSoulImage },
            { "Water Soul", _waterSoulImage },
            { "Poisonous Soul", _poisonousSoulImage },
            { "Evil Eye Soul", _evilEyeSoulImage },
            { "Ice Soul", _iceSoulImage }
        };
    }

    public void SetCurrentSoulImage(string soulName)
    {
        _soulImage.sprite = _soulsSprites[soulName];
        _soulImage.SetNativeSize();
    }

    public void SetNumberOfLives(int numberOfLives) => _numberOfHearts.text = "x" + numberOfLives;

    public void SetNumberOfSouls(int numberOfSouls) => _numberOfSouls.text = "x" + numberOfSouls;
}
