using System;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.UI
{
    public class FinishPanel : UIPanel
    {
        public override void ShowPanel()
        {
            base.ShowPanel();
            GetArea(FinishAreaType.FinishBGArea).ShowArea();
        }
    }
}
