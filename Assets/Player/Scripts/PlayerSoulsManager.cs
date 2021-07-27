using UnityEngine;

public class PlayerSoulsManager
{
    private const string BASE_SOUL_NAME = "Fire Soul";

    private int _numberOfSoulsToPlace;
    private int _currentNumberOfSoulsToPlace;
    private GameObject _baseSoul;
    public GameObject CurrentSoul { get; private set; }

    public void ReduceNumberOfSoulsToPlace()
    {
        _currentNumberOfSoulsToPlace--;

        if (!CanPlaceSoul() && !IsBaseSoul(CurrentSoul))
        {
            CurrentSoul = _baseSoul;
            _currentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
    }

    public void IncreaseNumberOfSoulsToPlace()
    {
        if (!IsBaseSoul(CurrentSoul))
        {
            return;
        }

        _currentNumberOfSoulsToPlace++;

        if (_currentNumberOfSoulsToPlace > _numberOfSoulsToPlace)
        {
            _currentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
    }

    public bool CanPlaceSoul() => _currentNumberOfSoulsToPlace > 0;

    public void ChangeBaseSoul(GameObject soul, int startingNumberOfSouls)
    {
        _baseSoul = soul;
        CurrentSoul = soul;

        _numberOfSoulsToPlace = startingNumberOfSouls;
        _currentNumberOfSoulsToPlace = startingNumberOfSouls;
    }

    public void ChangeSoul(GameObject soul, int numberOfUses)
    {
        if (IsBaseSoul(soul))
        {
            CurrentSoul = _baseSoul;
            _numberOfSoulsToPlace++;
            _currentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
        else
        {
            CurrentSoul = soul;
            _currentNumberOfSoulsToPlace = numberOfUses;
        }
    }

    private bool IsBaseSoul(GameObject soul) => soul.name.Contains(BASE_SOUL_NAME);
}
