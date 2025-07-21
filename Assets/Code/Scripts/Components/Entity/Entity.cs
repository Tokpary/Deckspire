using UnityEngine;

namespace Code.Scripts.Components.Entity
{
	public abstract class Entity : MonoBehaviour
	{
		public int MaxHealth {  get; set; }
		public int CurrentHealth { get; set; }
		public int MaxEnergy {get; set;}
		public int CurrentEnergy {get; set;}

		public int DamageMultiplier { get; set; } = 1;

		public virtual void Awake()
		{
			DamageMultiplier = 1;
			CurrentHealth = MaxHealth;
			CurrentEnergy = MaxEnergy;
		}

		public abstract void Die();
		
		public virtual void TakeDamage(int damage){
			CurrentHealth -= damage;
			if (CurrentHealth <= 0)
				Die();
		}
	}
}