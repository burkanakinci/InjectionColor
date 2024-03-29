using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.GamePlayer;
using Game.Manager;
using Game.UI;
using Game.Utilities.Constants;

namespace Game.StateMachine
{
    public class WinState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;
        private UIPanel m_FinisPanel;
        private UIManager m_UIManager;

        public WinState(Player _player)
        {
            m_Player = _player;
            m_UIManager = GameManager.Instance.GetManager<UIManager>();
            m_FinisPanel = m_UIManager.GetPanel(UIPanelType.FinishPanel);
        }
        
        public void Enter()
        {
            m_UIManager.HideAllPanels();
            m_FinisPanel.ShowPanel();
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
            m_FinisPanel.HidePanel();
            OnExitEvent?.Invoke();
        }
    }
}