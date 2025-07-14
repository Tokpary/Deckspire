namespace DefaultNamespace;

public class GameContext
{
    public Player currentPlayer;
	public Enemy targetEnemy;
    public ACard sourceCard;
    public ACard targetCard;
	public GameBoard gameBoard;
    public List<ACard> allCardsInHand;
    public List<ACard> allCardsInTable;

}