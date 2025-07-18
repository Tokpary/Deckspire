using System;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment
{
    public class DialogueManager : MonoBehaviour
    {
        public void DisplayDialogue(string dialogue, Action onComplete = null)
        {
            Fungus.Flowchart.BroadcastFungusMessage(dialogue);
        }
    }
}