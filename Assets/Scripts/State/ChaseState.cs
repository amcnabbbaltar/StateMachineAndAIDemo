using UnityEngine;

public class ChaseState : IState
{
    private AIController aiController;

    public StateType Type => StateType.Chase;
    
    private Renderer aiRenderer;
    private Color originalColor;
    private Color chaseColor = new Color(1f, 0.6470588f, 0f, 1f); // Color when it sees the player

    public ChaseState(AIController aiController)
    {
        this.aiController = aiController;
        aiRenderer = aiController.GetComponent<Renderer>();

        if (aiRenderer != null)
        {
            originalColor = aiRenderer.material.color;
        }
    }

    public void Enter()
    {
           if (aiRenderer != null)
            aiRenderer.material.color = chaseColor;
        // No animations, so no need to set any animator parameters
    }

    public void Execute()
    {
        if (!aiController.CanSeePlayer())
        {
            aiController.StateMachine.TransitionToState(StateType.Patrol);
            return;
        }

        if (aiController.IsPlayerInAttackRange())
        {
            aiController.StateMachine.TransitionToState(StateType.Attack);
            return;
        }

        aiController.Agent.destination = aiController.Player.position;
    }

    public void Exit()
    {
        if (aiRenderer != null)
            aiRenderer.material.color = originalColor;
        // No cleanup necessary
    }
}
