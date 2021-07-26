using UnityEngine;

public class PlayerSoulsManager
{
    private string BASE_SOUL_NAME = "Fire Soul";

    private int _numberOfSoulsToPlace;
    private int _currentNumberOfSoulsToPlace;
    private GameObject _baseSoul;
    public GameObject CurrentSoul { get; private set; }

    public PlayerSoulsManager(D_PlayerStats playerStatsData) => _numberOfSoulsToPlace = playerStatsData.startingNumberOfSouls;

    public void ReduceNumberOfSoulsToPlace()
    {
        _currentNumberOfSoulsToPlace--;

        if (!CanPlaceSoul())
        {
            CurrentSoul = _baseSoul;
            _currentNumberOfSoulsToPlace = _numberOfSoulsToPlace;
        }
    }

    public void IncreaseNumberOfSoulsToPlace() => _currentNumberOfSoulsToPlace++;

    public bool CanPlaceSoul() => _currentNumberOfSoulsToPlace > 0;

    public void ChangeBaseSoul(GameObject soul) => _baseSoul = soul;

    public void ChangeSoul(GameObject soul, int numberOfUses)
    {
        CurrentSoul = soul;
        _currentNumberOfSoulsToPlace = numberOfUses;
    }
}
