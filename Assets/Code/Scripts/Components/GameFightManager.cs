using System.Collections.Generic;
using Code.Scripts.Components.Entity;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components
{
    public class GameFightManager : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private string[] enemyIds;
        
        Dictionary<string, Enemy> enemyDictionary = new Dictionary<string, Enemy>();
        
        private void Start()
        {
            for (int i = 0; i < _enemies.Length; i++)
            {
                enemyDictionary.Add(enemyIds[i], _enemies[i]);
            }
        }

        public void SetHermitEnemy()
        {
            GameManager.Instance.Enemy = enemyDictionary["Hermit"];
            GameManager.Instance.InitializeNextEnemy();
        }
        
        public void SetWheelOfFortuneEnemy()
        {
            GameManager.Instance.Enemy = enemyDictionary["WheelOfFortune"];
            GameManager.Instance.InitializeNextEnemy();
        }
        
        
        public void SetDeathEnemy()
        {
            GameManager.Instance.Enemy = enemyDictionary["Death"];
            GameManager.Instance.InitializeNextEnemy();
        }

        public void CleanGameBoard()
        {
            GameManager.Instance.GameBoard.ClearBoard();
        }
    }
}