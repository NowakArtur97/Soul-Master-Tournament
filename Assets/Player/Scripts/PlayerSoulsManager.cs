using UnityEngine;

public class PlayerSoulsManager
{
    private const string BASE_SOUL_NAME = "Fire Soul";

    private int _numberOfSoulsToPlace;
    public int CurrentNumberOfSoulsToPlace { get; private set; }
    private GameObject _baseSoul;
    public GameObject CurrentSoul { get; private set; }

    public void ReduceNumberOfSoulsToPlace()
    {
        CurrentNumberOfSoulsToPlace--;

        if (!CanPlaceSoul() && !IsBaseSoul(CurrentSoul))
        {
            CurrentSoul = _baseSoul;
            CurrentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
    }

    public void IncreaseNumberOfSoulsToPlace()
    {
        if (!IsBaseSoul(CurrentSoul))
        {
            return;
        }

        CurrentNumberOfSoulsToPlace++;

        if (CurrentNumberOfSoulsToPlace > _numberOfSoulsToPlace)
        {
            CurrentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
    }

    public bool CanPlaceSoul() => CurrentNumberOfSoulsToPlace > 0;

    public void ChangeBaseSoul(GameObject soul, int startingNumberOfSouls)
    {
        _baseSoul = soul;
        CurrentSoul = soul;

        _numberOfSoulsToPlace = startingNumberOfSouls;
        CurrentNumberOfSoulsToPlace = startingNumberOfSouls;
    }

    public void ChangeSoul(GameObject soul, int numberOfUses)
    {
        if (IsBaseSoul(soul))
        {
            CurrentSoul = _baseSoul;
            _numberOfSoulsToPlace++;
            CurrentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
        else
        {
            CurrentSoul = soul;
            CurrentNumberOfSoulsToPlace = numberOfUses;
        }
    }

    private bool IsBaseSoul(GameObject soul) => soul.name.Contains(BASE_SOUL_NAME);
}
