using System;
using DG.Tweening;
using Game.Manager;
using Game.Object;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Game.UI
{
    public class TargetColorMatchArea : UIArea
    {
        [SerializeField] 
        private RectTransform m_AreaTransform;
        
        #region Tween Values

        [SerializeField] [FoldoutGroup("Tween Values")]
        private float m_MixedTweenDuration;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private Ease m_MixedTweenEase;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private float m_ShowTweenDuration;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private Ease m_ShowTweenEase;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private float m_HideTweenDuration;
        [SerializeField] [FoldoutGroup("Tween Values")]
        private Ease m_HideTweenEase;

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

        private float m_ScreenHeight;
        private Vector3 m_StartPos;
        private Vector3 m_EndPos;
        private PouringCupTarget m_PouringCupTarget;
        
        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);

            m_PouringCupTarget = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup.PouringCupTarget;
            
            m_MixedValue = 0;
            m_CurrentColorSlider.fillAmount=0.0f;
            m_MixedColorSlider.fillAmount=0.0f;
            m_PercentText.text = "0";

            m_EndPos = m_AreaTransform.anchoredPosition;
            m_StartPos = Vector3.up * -650.0f;
            m_AreaTransform.anchoredPosition = m_StartPos;
        }

        public void SetCurrentColor(Color _color)
        {
            m_CurrentColorSlider.color = _color;
        }

        public void SetTargetColor(Color _color)
        {
            m_MixedColorSlider.color = _color;
        }
        public Tween ShowTween()
        {
            return LocalMoveTween(m_EndPos, m_ShowTweenDuration).SetEase(m_ShowTweenEase);
        }

        public Tween HideTween()
        {
            return LocalMoveTween(m_StartPos, m_HideTweenDuration).SetEase(m_HideTweenEase);
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
        private Tween m_MoveTween;

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

        private Vector3 m_TweenStartPos;
        private Vector3 m_TweenEndPos;
        private Tween LocalMoveTween(Vector3 _pos, float _duration)
        {
            m_TweenStartPos = m_AreaTransform.anchoredPosition;
            m_TweenEndPos = _pos;
            m_MoveTween?.Kill();
            m_MoveTween = DOTween.To(()=>
                0.0f,
                _value => SetPosByLerp(_value),
                1.0f,
                _duration);
            return m_MoveTween;
        }

        private void SetPosByLerp(float _lerp)
        {
            m_AreaTransform.anchoredPosition = Vector3.Lerp(m_TweenStartPos,m_EndPos,_lerp);
        }

        public void KillAllTween()
        {
            m_MixedTween?.Kill();
            m_MoveTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
        }
    }
}