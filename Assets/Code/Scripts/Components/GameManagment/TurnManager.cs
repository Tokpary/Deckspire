using System.Collections;
using Code.Scripts.Components.Entity;
using Code.Scripts.Components.GameManagment.GameStates;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment
{
    public class TurnManager : MonoBehaviour
    {
        private GameBoard.GameBoard _board;
        private Player _player;
        private Enemy _enemy;

        private bool isPlayerTurn = true;

        public void StartGame()
        {

        }

        public void StartGame(GameBoard.GameBoard board, Player player, Enemy enemy)
        {
            _board = board;
            _player = player;
            _enemy = enemy;
        }

        public void EnemyTurn()
        {
            _enemy.PlayTurn(() =>
            {
                GameManager.Instance.GameFlowManager.SetState(new AfterEnemyState(GameManager.Instance.GameFlowManager));
            });
        }
        
    }
}