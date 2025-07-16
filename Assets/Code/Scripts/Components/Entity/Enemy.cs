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

		public void Die()
		{
			
		}

		public virtual void TakeDamage(int damage){
			
			CurrentHealth -= damage * DamageMultiplier;
			if (CurrentHealth <= 0)
				Die();
		}
	}
}