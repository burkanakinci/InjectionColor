using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using TMPro;
using UnityEngine;
namespace Game.UI
{
    public class LevelArea : UIArea
    {
        [SerializeField] private TextMeshProUGUI m_LevelText;
        private JsonConverter m_JSONConverter;
        private IPlayerState m_PlayerIdleState;
        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_JSONConverter = GameManager.Instance.GetManager<JsonConverter>();
            m_PlayerIdleState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine.GetPlayerState(PlayerStates.IdleState);
            m_PlayerIdleState.OnEnterEvent += OnEnterIdleState;
        }

        private void OnEnterIdleState()
        {
            m_LevelText.text = "Level " + m_JSONConverter.SavedPlayerData.PlayerLevel;
        }
    }
}

