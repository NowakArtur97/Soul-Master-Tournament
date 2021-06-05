public class PlayerSoulsManager
{
    private int _numberOfSoulsToPlace;

    public PlayerSoulsManager(D_PlayerStats playerStatsData) => _numberOfSoulsToPlace = playerStatsData.startingNumberOfSouls;

    public void ReduceNumberOfSoulsToPlace() => _numberOfSoulsToPlace--;

    public void IncreaseNumberOfSoulsToPlace() => _numberOfSoulsToPlace++;

    public bool CanPlaceSoul() => _numberOfSoulsToPlace > 0;
}
