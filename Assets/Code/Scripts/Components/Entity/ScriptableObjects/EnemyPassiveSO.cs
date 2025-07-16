namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    public abstract class EnemyPassiveSO : ScriptableObject
    {
        public abstract void ExecutePassive(Enemy enemy);
    }
}