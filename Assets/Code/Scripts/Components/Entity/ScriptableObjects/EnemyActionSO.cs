namespace DefaultNamespace;

public abstract class EnemyActionSO : ScriptableObject
{
    public abstract void ExecuteAction(Enemy enemy, GameContext gameContext);
}