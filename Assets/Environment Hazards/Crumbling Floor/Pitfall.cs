public class Pitfall : EnvironmentHazardActiveOnContact
{
    protected override void UseEnvironmentHazard() => Damage();
}
