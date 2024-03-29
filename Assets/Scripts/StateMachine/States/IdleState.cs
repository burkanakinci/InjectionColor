using UnityEngine;
using System;
using Game.GamePlayer;
using Game.Manager;
using Game.UI;
using Game.Utilities.Constants;

namespace Game.StateMachine
{
    public class IdleState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;
        private UIPanel m_IdlePanel;
        private UIPanel m_CommonPanel;
        private UIManager m_UIManager;

        public IdleState(Player _player)
        {
            m_Player = _player;
            m_UIManager = GameManager.Instance.GetManager<UIManager>();
            m_IdlePanel = m_UIManager.GetPanel(UIPanelType.IdlePanel);
            m_CommonPanel = m_UIManager.GetPanel(UIPanelType.CommonPanel);
        }

        public void Enter()
        {
            m_UIManager.HideAllPanels();
            m_CommonPanel.ShowPanel();
            m_IdlePanel.ShowPanel();
            GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine.ChangeStateTo(PlayerStates.RunState);
            GameManager.Instance.GetManager<LevelManager>().LoadLevel();
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
            m_IdlePanel.HidePanel();
            OnExitEvent?.Invoke();
        }
    }
}