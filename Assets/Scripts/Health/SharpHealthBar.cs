public class SharpHealthBar : HealthBar
{
    protected override void OnUpdateValue(float health)
    {
        HealthScale.value = health;
    }
}
