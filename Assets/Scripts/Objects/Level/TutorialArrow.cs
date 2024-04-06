using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Object
{
    public class TutorialArrow : CustomBehaviour<TutorialLevel>
    {
        #region Disable Tween Values

        [FoldoutGroup("Disable Tween Values")] [SerializeField]
        private float m_DisableTweenDuration;

        [FoldoutGroup("Disable Tween Values")] [SerializeField]
        private Ease m_DisableTweenEase;

        #endregion

        [SerializeField] private TextMeshPro m_ClickObjectText;
        [SerializeField] private GameObject m_ArrowObject;

        public void DisableArrow()
        {
            DisableTween().SetEase(m_DisableTweenEase);
        }

        #region Tween

        private Tween m_DisableTween;

        private Tween DisableTween()
        {
            m_DisableTween?.Kill();
            m_DisableTween = DOTween.To(() =>
                    0.0f,
                _value => DisableObjectsByLerp(_value),
                1.0f,
                m_DisableTweenDuration);
            return m_DisableTween;
        }

        private void DisableObjectsByLerp(float _lerpValue)
        {
            m_ArrowObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _lerpValue);
        }

        public void KillAllTween()
        {
            m_DisableTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
        }
    }
}