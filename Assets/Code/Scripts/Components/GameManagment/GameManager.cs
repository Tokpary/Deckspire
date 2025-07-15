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
    public class GameManager : DesignPatterns.Singleton<GameManager>, IGameState
    {
        private GameFlowManager _gameFlowManager;
        private TurnManager _turnManager;
        
        public GameBoard.GameBoard _gameBoard;
        
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
            _gameBoard = new GameBoard.GameBoard();
            _turnManager = GetComponent<TurnManager>();
            _gameFlowManager = GetComponent<GameFlowManager>();
        }
        
        private void Start()
        {
            InitializeGame();
            Debug.Log(_player);
            _gameBoard.Initialize(this);
            _turnManager.StartGame(gameBoard, _player, _enemy);
           
        }

        private void InitializeGame()
        {
           // player.HandDeck.Initialize();
        }

        public void StartGame()
        {
            _gameFlowManager.SetState(new DrawState(this));
        }

        public void EndGame()
        {
            // Logic to end the game, e.g., show game over screen, reset game state, etc.
            Debug.Log("Game Over");
            ShowMainMenu();
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