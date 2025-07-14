namespace DefaultNamespace;

public class Enemy : Entity
{
    [SerializeField] private EntitySO _enemyData;

	public void PlayTurn(){
		int index = Random.Range(0, _enemyData.Actions.length);
		_enemyData.Actions[index].ExecuteAction(this, gameContext);
	}
}