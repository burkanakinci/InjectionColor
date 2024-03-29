using System;
using System.Linq;
using DG.Tweening;
using Game.Manager;
using Game.Object;
using Game.StateMachine;
using Game.Utilities.Constants;
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

        #region Mixed Tweens

        [SerializeField] [FoldoutGroup("Mixed Tween Values")]
        private float m_MixedTweenDuration;
        [SerializeField] [FoldoutGroup("Mixed Tween Values")]
        private Ease m_MixedTweenEase;

        #endregion

        #region Show Hide Tweens

        [SerializeField] [FoldoutGroup("Show Tween Values")]
        private float m_ShowTweenDuration;
        [SerializeField] [FoldoutGroup("Show Tween Values")]
        private Ease m_ShowTweenEase;
        [SerializeField] [FoldoutGroup("Show Tween Values")]
        private float m_HideTweenDuration;
        [SerializeField] [FoldoutGroup("Show Tween Values")]
        private Ease m_HideTweenEase;

        #endregion

        #region Button Tweens

        [SerializeField] [FoldoutGroup("Button Tween Values")]
        private ButtonScaleTweenPair m_ScaleUpButtonPair;
        [SerializeField] [FoldoutGroup("Button Tween Values")]
        private ButtonScaleTweenPair m_ScaleDownButtonPair;

        #endregion

        #endregion
        
        #region Images

        [SerializeField][FoldoutGroup("Images")] 
        private Image[] m_MatchingColorImages;
        [SerializeField][FoldoutGroup("Images")] 
        private Image m_CurrentColorSlider;
        [SerializeField][FoldoutGroup("Images")] 
        private Image m_MixedColorSlider;

        #endregion

        #region Buttons

        [SerializeField] [FoldoutGroup("Buttons")]
        private ContinueInjectButton m_ContinueInjectButton;
        [SerializeField] [FoldoutGroup("Buttons")]
        private NextLevelButton m_NextLevelButton;

        #endregion
    
        #region Text

        [SerializeField] 
        private TextMeshProUGUI m_PercentText;

        #endregion

        [SerializeField] 
        private WinPercentPair[] m_WinPercentPairs; 
        private PlayerStateMachine m_PlayerStateMachine;

        private float m_ScreenHeight;
        private Vector3 m_StartPos;
        private Vector3 m_EndPos;
        private PouringCupTarget m_PouringCupTarget;
        private UIPanel m_FinishPanel;

        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            
            m_ContinueInjectButton.Initialize(this);
            m_NextLevelButton.Initialize(this);

            m_PouringCupTarget = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup.PouringCupTarget;
            
            m_MixedValue = 0;
            m_CurrentColorSlider.fillAmount=0.0f;
            m_MixedColorSlider.fillAmount=0.0f;
            m_PercentText.text = "0";

            m_EndPos = m_AreaTransform.anchoredPosition;
            m_StartPos = Vector3.up * -650.0f;
            m_AreaTransform.anchoredPosition = m_StartPos;

            m_WinPercentPairs = m_WinPercentPairs.OrderBy(_opened => _opened.OpenedPercentValue).ToArray();

            m_ContinueInjectButton.transform.localScale = Vector3.zero;
            m_NextLevelButton.transform.localScale = Vector3.zero;
            
            m_PlayerStateMachine = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine;

            m_FinishPanel = GameManager.Instance.GetManager<UIManager>().GetPanel(UIPanelType.FinishPanel);
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
            return LocalMoveTween(m_StartPos, m_HideTweenDuration).SetEase(m_HideTweenEase).
                OnComplete(() =>
                {
                    HideArea();
                });
        }

        public void ContinueInject()
        {
            m_ContinueInjectButton.ScaleDownTween(m_ScaleDownButtonPair);
            m_NextLevelButton.ScaleDownTween(m_ScaleDownButtonPair);
            HideTween();
            m_PlayerStateMachine.ChangeStateTo(PlayerStates.RunState);
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

            MixedTween(m_MixedTweenDuration).SetEase(m_MixedTweenEase)
                .OnComplete(() =>
                {
                    OnCompleteMixed();
                });
        }

        private void OnCompleteMixed()
        {
            m_ContinueInjectButton.ScaleUpTween(m_ScaleUpButtonPair);
            if (m_MixedValue >= m_WinPercentPairs[0].OpenedPercentValue)
            {
                m_NextLevelButton.ScaleUpTween(m_ScaleUpButtonPair);
            }
        }

        public void OnClickedNextLevel()
        {
            transform.SetParent(m_FinishPanel.transform);
            m_ContinueInjectButton.ScaleDownTween(m_ScaleDownButtonPair);
            m_NextLevelButton.ScaleDownTween(m_ScaleDownButtonPair);
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
            m_AreaTransform.anchoredPosition = Vector3.Lerp(m_TweenStartPos,m_TweenEndPos,_lerp);
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

    [Serializable]
    public struct WinPercentPair
    {
        #region Datas

        [SerializeField] 
        private float m_OpenedPercentValue;

        #endregion

        #region External Access

        public float OpenedPercentValue => m_OpenedPercentValue;

        #endregion
    }

    [Serializable]
    public struct ButtonScaleTweenPair
    {
        #region Datas
        
        [SerializeField] 
        private float m_ScaleTweenDuration;
        [SerializeField] 
        private Ease m_ScaleTweenEase;

        #endregion

        #region External Access
        
        public float ScaleTweenDuration => m_ScaleTweenDuration;
        public Ease ScaleTweenEase => m_ScaleTweenEase;

        #endregion
    }
}