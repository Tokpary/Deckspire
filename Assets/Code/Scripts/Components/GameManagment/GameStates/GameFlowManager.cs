namespace DefaultNamespace;

public class GameFlowManager : MonoBehaviour, IGameState
{
    private IState _currentState;
    private IState _previousState;
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
}