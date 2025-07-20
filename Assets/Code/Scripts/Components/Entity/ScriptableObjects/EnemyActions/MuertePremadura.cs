using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "MuertePremadura", menuName = "ScriptableObjects/Entity/EnemyAction/MuertePremadura", order = 1)]
    public class MuertePremadura : EnemyActionSO
    {
        public int CardsAmount = 2;

        public override void ExecuteAction(System.Action onComplete = null)
        {
            base.ExecuteAction();
            GameManager.Instance.GameBoard.SendFromStackToDiscard(CardsAmount);
            onComplete?.Invoke();
        }
    }
}