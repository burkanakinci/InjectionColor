using System;

namespace Game.StateMachine
{
    public interface IPlayerState
    {
        void Enter();
        void UpdateLogic();
        void UpdatePhysic();
        void Exit();
        Action OnEnterEvent { get; set; }
        Action OnExitEvent { get; set; }
    }
}
