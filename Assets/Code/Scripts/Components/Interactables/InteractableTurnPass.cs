namespace DefaultNamespace;

public class InteractableTurnPass : IInteractableObject
{
    public void Activate()
    {
        if(GameManager.Instance.GetCurrentState() is DeployCardState);
            GameManager.Instance.SetState(new EnemyActionState());
    }

    public void Highlight()
    {
        
    }
}