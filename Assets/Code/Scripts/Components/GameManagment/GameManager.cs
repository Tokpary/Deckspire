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
        
        private IState _currentState;
        private IState _previousState;
        private TurnManager _turnManager;
        
        public GameBoard.GameBoard gameBoard;
        
        [SerializeField] Player _player;
        public Player Player
        {
            get => _player;
            set => _player = value;
        }
        
        [SerializeField] public Enemy _enemy;
        public Enemy Enemy
        {
            get => _enemy;
            set => _enemy = value;
        }
        
        private void Awake()
        {
            base.Awake();
            _currentState = null;
            _previousState = null;
            gameBoard = new GameBoard.GameBoard();
            _turnManager = GetComponent<TurnManager>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "GameLevel")
            {
                StartGame();
            }
        }
        
        private void Start()
        {
            InitializeGame();
            Debug.Log(_player);
            gameBoard.Initialize(this);
            _turnManager.StartGame(gameBoard, _player, _enemy);
            
            SetState(new DrawState(this));
        }

        private void InitializeGame()
        {
           // player.HandDeck.Initialize();
        }

        public void StartGame()
        {
            // Logic to start the game, e.g., initialize game board, player, enemy, etc.
            Debug.Log("Game Started");
            SetState(new DrawState(this));
        }

        public void EndGame()
        {
            // Logic to end the game, e.g., show game over screen, reset game state, etc.
            Debug.Log("Game Over");
            ShowMainMenu();
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }

        public void SetState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }

            _previousState = _currentState;
            _currentState = state;
            _currentState.Enter(this);
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