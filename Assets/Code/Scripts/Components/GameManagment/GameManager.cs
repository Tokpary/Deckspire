namespace DefaultNamespace;

public class GameManager : Singleton<GameManager>
{
    public GameBoard gameBoard;
    public Player player;
    public Enemy enemy;
    public TurnManager turnManager;

    private void Start()
    {
        InitializeGame();
        turnManager.StartGame();
    }

    private void InitializeGame()
    {
        player.handDeck.Initialize();
    }
}