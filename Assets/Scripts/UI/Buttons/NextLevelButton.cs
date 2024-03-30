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
        public override void Initialize(TargetColorMatchArea _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            OnClickTargetMatchAreaButton = NextLevel;
        }
        private void NextLevel()
        {
            CachedComponent.OnClickedNextLevel();
        }
    }
}
