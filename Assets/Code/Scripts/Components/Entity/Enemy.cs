using Code.Scripts.Components.Entity.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity
{
	public class Enemy : Entity
	{
		[SerializeField] private EntitySO _enemyData;

		public void PlayTurn(){
			_enemyData.Actions[Random.Range(0, _enemyData.Actions.Count)].ExecuteAction(this);
		}
	}
}