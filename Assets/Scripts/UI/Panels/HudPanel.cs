using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;

namespace Game.UI
{
    public class HudPanel : UIPanel
    {
        private IPlayerState m_RunState;

        public override void Initialize()
        {
            base.Initialize();
            m_RunState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine
                .GetPlayerState(PlayerStates.RunState);
            m_RunState.OnEnterEvent += ShowPanel;
        }

        private void OnDisable()
        {
            m_RunState.OnEnterEvent -= ShowPanel;
        }
    }
}