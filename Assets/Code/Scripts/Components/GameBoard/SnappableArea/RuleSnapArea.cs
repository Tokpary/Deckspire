using System;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class RuleSnapArea : ASnapZone
    {
        Transform _snapPoint;

        private void Awake()
        {
            _snapPoint = transform;
        }

        public override bool CanAcceptCard(ACard card)
        {
            bool cleanse = false;
            foreach (var ability in card.GetDataCard().abilities)
            {
                if (ability.name == "CleanseAbility") // Assuming 4 is the type for cleanse ability
                {
                    cleanse = true;
                    GameManager.Instance.GameBoard.GameRulesData.IsCleanseApplied = true;
                }
            }
            
            if (_currentCardOnSlot != null && !cleanse)
            {
                return false;
            }
            
            if ((card.GetDataCard().cardType == 2 || (cleanse && _currentCardOnSlot != null)) && card.EnergyCost <= GameManager.Instance.Player.CurrentEnergy)
            {
                return true;
            }

            return false;
        }

        public override void SnapCard(ACard card)
        {
            if (card == null) return;

            if (CanAcceptCard(card))
            {
                if (GameManager.Instance.GameBoard.GameRulesData.DecoyOnTableCards)
                {
                    card.LifeTime = 3;
                    card.UpdateCard();
                }
                if(GameManager.Instance.GameBoard.GameRulesData.IsCleanseApplied)
                {
                    GameManager.Instance.GameBoard.GameRulesData.IsCleanseApplied = false;
                    GameManager.Instance.GameBoard.MoveCardToDiscard(_currentCardOnSlot);
                    GameManager.Instance.GameBoard.MoveCardToDiscard(card);
                    GameManager.Instance.Player.CurrentEnergy -= card.EnergyCost;
                    GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
                    _currentCardOnSlot.PlayOnExitCard();
                    _currentCardOnSlot = null;
                    return; 
                }
                card.transform.position = _snapPoint.position;
                _currentCardOnSlot = card;
                card.CurrentSnapZone = this;
                GameManager.Instance.Player.CurrentEnergy -= card.EnergyCost;
                GameManager.Instance.GameBoard.DisplayCardInTable(card);
                card.PlayCard();
            }
        }
        
        
    }
}