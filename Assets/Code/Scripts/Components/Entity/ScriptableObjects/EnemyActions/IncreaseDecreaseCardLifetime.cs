namespace DefaultNamespace;

public class IncreaseDecreaseCardLifetime : EnemyActionSO
{
    public override void ExecuteAbility(Enemy enemy, GameContext context)
    {
        foreach (ACard card in context.allCardsInPlay)
        {
            card.LifeTime += Random.Range(-1, 1)
        }
    }
}