using UnityEngine;
using System;
using Game.Object;

namespace Game.UI
{
    public class UIArea : CustomBehaviour<UIPanel>
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;
        public CanvasGroup CanvasGroup => m_CanvasGroup;

        public virtual void ShowArea()
        {
            CanvasGroup.Open();
        }

        public virtual void ShowArea(Action _completed)
        {
            CanvasGroup.Open();
        }

        public virtual void HideArea()
        {
            CanvasGroup.Close();
        }
    }
}
