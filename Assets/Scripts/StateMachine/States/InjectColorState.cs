using UnityEngine;
using System;
using Game.GamePlayer;

namespace Game.StateMachine
{
    public class InjectColorState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;
        public InjectColorState(Player _player)
        {
            m_Player = _player;
        }

        public void Enter()
        {
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