using TMPro;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private TMP_Text turnText;
        
        public void UpdateEnergy(int energy)
        {
            if (energyText != null)
            {
                energyText.text = $"{energy}";
            }
        }
        
        public void UpdateTurn(int turn)
        {
            if (turnText != null)
            {
                turnText.text = $"{turn}";
            }
        }
    }
}