using System;
using Code.Scripts.Components.Entity.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.Entity
{
	public class Enemy : Entity
	{
		[SerializeField] private EntitySO _enemyData;

		public override void Awake()
		{
			MaxHealth = _enemyData.MaxHealth;
			CurrentHealth = MaxHealth;
		}

		public void PlayTurn(Action onComplete = null){
			
			_enemyData.Actions[Random.Range(0, _enemyData.Actions.Count)].ExecuteAction(() =>
			{
				onComplete?.Invoke();
			});
		}

		public override void TakeDamage(int damage)
		{
			if (DamageMultiplier != 1)
			{
				damage *= DamageMultiplier;
				DamageMultiplier = 1; 
			}
			base.TakeDamage(damage);
		}

		public override void Die()
		{
			foreach (var dieEvent in _enemyData.DieEvents)
			{
				dieEvent.OnDieEvent();
			}

			CurrentHealth = MaxHealth;
		}

	}
}