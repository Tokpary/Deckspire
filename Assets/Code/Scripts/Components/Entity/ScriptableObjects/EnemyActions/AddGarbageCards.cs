namespace DefaultNamespace;

public class AddGarbageCards : EnemyActionSO
{
    public override void ExecuteAction(Enemy enemy, GameContext gameContext)
    {
        gameContext.currentPlayer.HandDeck
    }
}