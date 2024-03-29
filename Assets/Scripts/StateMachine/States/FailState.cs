using UnityEngine;
using System;
using Game.GamePlayer;

namespace Game.StateMachine
{
    public class FailState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;
        public FailState(Player _player)
        {
            m_Player = _player;
        }

        public void Enter()
        {
            OnEnterEvent?.Invoke();
        }
        public void UpdateLogic()
        {
        }
        public void UpdatePhysic()
        {
        }
        public void Exit()
        {
            OnExitEvent?.Invoke();
        }
    }
}
