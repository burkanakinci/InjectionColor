using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TargetColorMatchArea : UIArea
    {
        #region Tween Values

        [SerializeField] [FoldoutGroup("Tween Values")]
        private float m_MixedTweenDuration;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private Ease m_MixedTweenEase;

        #endregion
        
        #region Images

        [SerializeField][FoldoutGroup("Images")] 
        private Image[] m_MatchingColorImages;
        [SerializeField][FoldoutGroup("Images")] 
        private Image m_CurrentColorSlider;
        [SerializeField][FoldoutGroup("Images")] 
        private Image m_MixedColorSlider;

        #endregion

        #region Text

        [SerializeField] 
        private TextMeshProUGUI m_PercentText;

        #endregion

        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_MixedValue = 0;
            m_CurrentColorSlider.fillAmount=0.0f;
            m_MixedColorSlider.fillAmount=0.0f;
            m_PercentText.text = "0";
        }

        private float m_MixedValue;
        private float m_CurrentSliderValue;
        private float m_StartCurrentSliderValue;
        private float m_MixedSliderValue;
        private float m_StartMixedSliderValue;
        private float m_StartMixedValue;
        public void SetMatchingPercent(float _mixedValue)
        {
            m_StartMixedValue = m_MixedValue;
            m_StartCurrentSliderValue = m_CurrentColorSlider.fillAmount;
            m_StartMixedSliderValue = m_MixedColorSlider.fillAmount;
            m_MixedValue = (int)_mixedValue;
            m_CurrentSliderValue = m_MixedValue / 100.0f;
            m_MixedSliderValue = m_CurrentSliderValue;

            MixedTween(m_MixedTweenDuration).SetEase(m_MixedTweenEase);
        }

        #region Tween

        private Tween m_MixedTween;

        private Tween MixedTween(float _duration)
        {
            m_MixedTween?.Kill();
            m_MixedTween = m_MixedTween = DOTween.To(()=>
                0.0f,
                _value => SetMixedUIByLerp(_value),
                1.0f,
                _duration);
            return m_MixedTween;
        }

        private void SetMixedUIByLerp(float _lerp)
        {
            m_CurrentColorSlider.fillAmount=Mathf.Lerp(m_StartCurrentSliderValue,m_CurrentSliderValue,_lerp);
            m_MixedColorSlider.fillAmount=Mathf.Lerp(m_StartMixedSliderValue,m_MixedSliderValue,_lerp);
            m_PercentText.text = ((int)Mathf.Lerp(m_StartMixedValue, m_MixedValue, _lerp)).ToString();
        }

        #endregion
    }
}