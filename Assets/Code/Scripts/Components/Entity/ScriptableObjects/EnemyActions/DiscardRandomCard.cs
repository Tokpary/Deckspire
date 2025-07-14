namespace DefaultNamespace;

public class DiscardRandomCard : EnemyActionSO
{
    public override void ExecuteAction(GameContext gameContext)
    {
        int index = Random.Range(0, gameContext.allCardsInHand.length);
        gameContext.allCardsInHand[index].Discard();
    }
}