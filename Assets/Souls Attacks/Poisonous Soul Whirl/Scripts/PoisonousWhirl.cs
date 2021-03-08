using UnityEngine;

public class PoisonousWhirl : SoulAbility
{
    protected override void Update()
    {
        if (Time.time >= StartTime + AbilityStats.activeTime)
        {
            HasFinished = true;
        }

        base.Update();
    }
}
