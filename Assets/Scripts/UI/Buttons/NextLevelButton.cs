using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.UI
{
    public class NextLevelButton : TargetColorMatchAreaButton
    {
        private PlayerStateMachine m_PlayerStateMachine;
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_PlayerStateMachine = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine;
            OnClickTargetMatchAreaButton = NextLevel;
        }
        private void NextLevel()
        {
            m_PlayerStateMachine.ChangeStateTo(PlayerStates.WinState);
        }
    }
}
