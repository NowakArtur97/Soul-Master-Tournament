public class FireSoulExplosion : Explosion
{
    private void Update()
    {
        if (HasExploded)
        {
            Destroy(gameObject);
        }
    }
}
