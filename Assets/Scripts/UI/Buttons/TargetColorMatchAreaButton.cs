using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class TargetColorMatchAreaButton : UIBaseButton<TargetColorMatchArea>
    {
        public Action OnClickTargetMatchAreaButton;
        protected override void OnClickAction()
        {
            OnClickTargetMatchAreaButton?.Invoke();
        }

        public void SetInteractable(bool _isInteractable)
        {
            m_Button.interactable = _isInteractable;
        }

        public void ScaleUpTween(ButtonScaleTweenPair _tweenPair)
        {
            ScaleTween(1.0f,_tweenPair.ScaleTweenDuration).SetEase(_tweenPair.ScaleTweenEase);
        }
        public void ScaleDownTween(ButtonScaleTweenPair _tweenPair)
        {
            ScaleTween(0.0f,_tweenPair.ScaleTweenDuration).SetEase(_tweenPair.ScaleTweenEase);
        }

        #region Tween

        private Tween m_ScaleTween;

        private Tween ScaleTween(float _scaleMultiply, float _duration)
        {
            m_ScaleTween?.Kill();
            m_ScaleTween = transform.DOScale(_scaleMultiply*Vector3.one,_duration);
            return m_ScaleTween;
        }

        public void KillAllTween()
        {
            m_ScaleTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
            OnClickTargetMatchAreaButton = null;
        }
    }
}