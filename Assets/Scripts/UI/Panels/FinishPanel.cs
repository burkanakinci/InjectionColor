using System;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;

namespace Game.UI
{
    public class FinishPanel : UIPanel
    {
        private IPlayerState m_WinState;
        public override void Initialize()
        {
            base.Initialize();
            m_WinState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine
                .GetPlayerState(PlayerStates.WinState);
            m_WinState.OnEnterEvent += ShowPanel;
        }

        private void OnDisable()
        {
            m_WinState.OnEnterEvent -= ShowPanel;
        }
    }
}
