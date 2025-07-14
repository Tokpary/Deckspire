using UnityEngine;

namespace Code.Scripts.Components.Entity
{
	public class Entity : MonoBehaviour
	{
		public int MaxHealth {  get; set; }
		public int CurrentHealth { get; set; }
		public int MaxEnergy {get; set;}
		public int CurrentEnergy {get; set;}

		public virtual void TakeDamage(int damage){
			CurrentHealth -= damage;
		}
	}
}