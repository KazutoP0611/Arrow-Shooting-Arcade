using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0 || player.moveInput.y != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
