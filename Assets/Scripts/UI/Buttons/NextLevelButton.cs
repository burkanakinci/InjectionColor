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
        private JsonConverter m_JsonConverter;
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_JsonConverter = GameManager.Instance.GetManager<JsonConverter>();
            OnClickTargetMatchAreaButton = NextLevel;
        }
        private void NextLevel()
        {
            m_JsonConverter.SavePlayerData(new PlayerData
            {
                PlayerLevel =  m_JsonConverter.SavedPlayerData.PlayerLevel + 1,
            });
            CachedComponent.OnClickedNextLevel();
        }
    }
}
