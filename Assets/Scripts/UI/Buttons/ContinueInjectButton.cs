using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.StateMachine;
using UnityEngine;
using AudioType = Game.Manager.AudioType;

namespace Game.UI
{
    public class ContinueInjectButton : TargetColorMatchAreaButton
    {
        private AudioManager m_AudioManager;
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_AudioManager = GameManager.Instance.GetManager<AudioManager>();
            OnClickTargetMatchAreaButton = ContinueInject;
        }

        private void ContinueInject()
        {
            m_AudioManager.Play(AudioType.ClickedContinueInjectButton);
            CachedComponent.ContinueInject();
        }
    }
}
