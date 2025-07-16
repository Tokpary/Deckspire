using Code.Scripts.Components.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.Components.Interactables
{
    public abstract class AInteractable : MonoBehaviour, IInteractableObject, IPointerClickHandler
    {
        public abstract void Activate();

        public abstract void Highlight();

        public void OnPointerClick(PointerEventData eventData)
        {
            Activate();
        }
    }
}