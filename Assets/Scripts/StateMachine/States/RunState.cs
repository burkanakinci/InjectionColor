﻿using UnityEngine;
using System;
using Game.GamePlayer;

namespace Game.StateMachine
{
    public class RunState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;

        public RunState(Player _player)
        {
            m_Player = _player;
        }

        public void Enter()
        {
            // GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel).ShowPanel();
            // ResetRayCollidedEvent();
            // OnEnterEvent?.Invoke();
        }
        
        public void UpdateLogic()
        {
        }

        public void UpdatePhysic()
        {
        }

        public void Exit()
        {
        }
    }
}
