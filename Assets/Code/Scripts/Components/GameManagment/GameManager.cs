using Code.Scripts.Components.Entity;
using Code.Scripts.Components.GameBoard;
using Code.Scripts.Components.GameManagment.GameStates;
using Patterns.State.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using IState = Patterns.State.Interfaces.IState;

namespace Code.Scripts.Components.GameManagment
{
    public class GameManager : DesignPatterns.Singleton<GameManager>
    {
        private GameFlowManager _gameFlowManager;
        public GameFlowManager GameFlowManager
        {
            get => _gameFlowManager;
            set => _gameFlowManager = value;
        }
        private TurnManager _turnManager;
        
        public GameBoard.GameBoard GameBoard;
        
        [SerializeField] Player _player;
        public Player Player
        {
            get => _player;
            set => _player = value;
        }
        
        [SerializeField] Enemy _enemy;
        public Enemy Enemy
        {
            get => _enemy;
            set => _enemy = value;
        }
        
        private void Awake()
        {
            base.Awake();
            _turnManager = GetComponent<TurnManager>();
            _gameFlowManager = GetComponent<GameFlowManager>();
        }
        
        private void Start()
        {
            InitializeGame();
            GameBoard.Initialize(this);
            _gameFlowManager.SetState(new DrawState(_gameFlowManager));
            _turnManager.StartGame(GameBoard, _player, _enemy);
        }

        private void InitializeGame()
        {
           // player.HandDeck.Initialize();
        }
        
        

        public void ShowMainMenu()
        {
            // Logic to show the main menu UI
            Debug.Log("Showing Main Menu");
        }
        
        public void HideMainMenu()
        {
            // Logic to show the main menu UI
            Debug.Log("Showing Main Menu");
        }
    }
}