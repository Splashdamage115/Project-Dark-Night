using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(StateMachine sm);
    public abstract void update(StateMachine sm);
    public abstract void ExitState(StateMachine sm);
}
