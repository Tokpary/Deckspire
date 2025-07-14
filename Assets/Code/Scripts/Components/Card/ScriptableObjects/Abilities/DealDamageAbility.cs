namespace DefaultNamespace;

public class DealDamageAbility : CardAbilitySO
{
    public int damage;

    public override void Activate(ACard card, GameContext gameContext)
    {
        gameContext.targetEnemy.TakeDamage(damage);
    }
}