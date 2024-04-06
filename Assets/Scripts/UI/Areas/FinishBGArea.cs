using Game.Manager;
using UnityEngine;

namespace Game.UI
{
    public class FinishBGArea : UIArea
    {
        [SerializeField] private RectTransform m_BGTransform;
        [SerializeField] private FinishBGMask m_FinishBGMask;
        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_FinishBGMask.Initialize();
            m_BGTransform.sizeDelta = new Vector2(
                Screen.width,
                Screen.height
                );
        }
    }
}
