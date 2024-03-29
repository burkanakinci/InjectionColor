using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.StateMachine;
using UnityEngine;

namespace Game.UI
{
    public class ContinueInjectButton : TargetColorMatchAreaButton
    {
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            OnClickTargetMatchAreaButton = ContinueInject;
        }

        private void ContinueInject()
        {
            CachedComponent.ContinueInject();
        }
    }
}
