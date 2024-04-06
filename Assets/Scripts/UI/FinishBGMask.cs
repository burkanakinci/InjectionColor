using DG.Tweening;
using Game.Object;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.UI
{
    public class FinishBGMask : CustomBehaviour
    {
        [SerializeField] private RectTransform m_MaskTransform;

        #region Tween Values

        #region Show BG Tween Value

        [SerializeField] [FoldoutGroup("Show BG Tween Value")]
        private float m_ShowBGDuration;
        [SerializeField] [FoldoutGroup("Show BG Tween Value")]
        private Ease m_ShowBGEase;

        #endregion
        
        #region Hide BG Tween Value

        [SerializeField] [FoldoutGroup("Hide BG Tween Value")]
        private float m_HideBGDuration;
        [SerializeField] [FoldoutGroup("Hide BG Tween Value")]
        private Ease m_HideBGEase;

        #endregion

        #endregion
        
        private float m_ScreenHeight;
        public override void Initialize()
        {
            m_ScreenHeight = Screen.height *2.0f;
            m_MaskTransform.sizeDelta = Vector2.one * m_ScreenHeight;
        }
        
        [Button]
        public void ShowBG()
        {
            MaskSizeTween(0.0f,m_ShowBGDuration).SetEase(m_ShowBGEase);
        }
        
        [Button]
        public void HideBG()
        {
            MaskSizeTween(m_ScreenHeight,m_HideBGDuration).SetEase(m_HideBGEase);
        }

        #region Tween

        private Tween m_MaskSizeTween;
        private float m_SizeStartMultiply;
        private float m_SizeTargetMultiply;

        private Tween MaskSizeTween(float _sizeMultiply, float _duration)
        {
            m_SizeStartMultiply=m_MaskTransform.sizeDelta.x;
            m_SizeTargetMultiply = _sizeMultiply;
            m_MaskSizeTween?.Kill();
            m_MaskSizeTween = DOTween.To(()=>
                0.0f,
                _value => SetMaskSizeByLerp(_value),
                1.0f,
                _duration);
            return m_MaskSizeTween;
        }

        private void SetMaskSizeByLerp(float _lerp)
        {
            m_MaskTransform.sizeDelta = Vector2.one * Mathf.Lerp(m_SizeStartMultiply,m_SizeTargetMultiply,_lerp);
        }

        #endregion
    }
}