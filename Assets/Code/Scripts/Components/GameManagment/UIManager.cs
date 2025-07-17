using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private TMP_Text turnText;
        [SerializeField] private Light spotlight;
        
        public void UpdateEnergy(int energy)
        {
            if (energyText != null)
            {
                energyText.text = $"{energy}";
                if (spotlight != null)
                {
                    spotlight.DOIntensity(energy / 1f, 0.5f).SetEase(Ease.InOutQuad);
                }
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