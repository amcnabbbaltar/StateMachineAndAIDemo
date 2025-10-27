using UnityEngine;

public class PatrolState : IState
{
    private AIController aiController;
    private int currentWaypointIndex = 0;

    private Renderer aiRenderer;
    private Color originalColor;
    private Color patrolColor = new Color(0f, 1f, 0f, 1f); // Color when it sees the player


    public StateType Type => StateType.Patrol;

    public PatrolState(AIController aiController)
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
            aiRenderer.material.color = patrolColor;
        //aiController.Animator.SetBool("isMoving", true);
        MoveToNextWaypoint();
    }

    public void Execute()
    {
        if (aiController.CanSeePlayer())
        {
            aiController.StateMachine.TransitionToState(StateType.Chase);
            return;
        }

        if (!aiController.Agent.pathPending && aiController.Agent.remainingDistance <= aiController.Agent.stoppingDistance)
        {
            MoveToNextWaypoint();
        }
    }

    public void Exit()
    {
        if (aiRenderer != null)
            aiRenderer.material.color = originalColor;
        // Cleanup if necessary
    }

    private void MoveToNextWaypoint()
    {
        if (aiController.Waypoints.Length == 0)
            return;

        aiController.Agent.destination = aiController.Waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % aiController.Waypoints.Length;
    }
}
