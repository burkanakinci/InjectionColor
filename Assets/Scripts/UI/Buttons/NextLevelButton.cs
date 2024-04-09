using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using UnityEngine;
using AudioType = Game.Manager.AudioType;

namespace Game.UI
{
    public class NextLevelButton : TargetColorMatchAreaButton
    {
        private JsonConverter m_JsonConverter;
        private AudioManager m_AudioManager;
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_JsonConverter = GameManager.Instance.GetManager<JsonConverter>();
            m_AudioManager = GameManager.Instance.GetManager<AudioManager>();
            OnClickTargetMatchAreaButton = NextLevel;
        }
        private void NextLevel()
        {
            m_AudioManager.Play(AudioType.ClickedNextLevelButton);
            m_JsonConverter.SavePlayerData(new PlayerData
            {
                PlayerLevel =  m_JsonConverter.SavedPlayerData.PlayerLevel + 1,
            });
            CachedComponent.OnClickedNextLevel();
        }
    }
}
