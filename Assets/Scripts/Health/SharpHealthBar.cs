public class SharpHealthBar : HealthBar
{
    protected override void UpdateValueHealthBar(float health)
    {
        HealthScale.value = health;
    }
}
