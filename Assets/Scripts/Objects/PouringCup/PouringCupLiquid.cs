using System;
using DG.Tweening;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupLiquid : CustomBehaviour<PouringCupVisual>
    {
        [SerializeField] private MeshRenderer m_PouringLiquidRenderer;
        private bool m_AnyMixed;
        public Color CurrentLiquidColor => m_TargetColor;
        
        public override void Initialize(PouringCupVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_AnyMixed = false;
        }

        private Color m_AddedColor;
        private Color m_TargetColor;
        private Color m_StartColor;
        private float m_TargetColorMultiply;
        [Button]
        public void SetTargetColorOnDeinject(Color _addedColor)
        {
            m_AddedColor = _addedColor;
            m_AddedColor.a = 1.0f;
            if (m_AnyMixed)
            {
                m_AddedColor.r *= 0.5f;
                m_AddedColor.g *= 0.5f;
                m_AddedColor.b *= 0.5f;

                m_TargetColor.r *= 0.5f;
                m_TargetColor.g *= 0.5f;
                m_TargetColor.b *= 0.5f;
            
                m_TargetColor.r += m_AddedColor.r;
                m_TargetColor.g += m_AddedColor.g;
                m_TargetColor.b += m_AddedColor.b;
            }
            else
            {
                m_TargetColor = m_AddedColor;
            }
            m_TargetColor.a = 1.0f;
            m_AnyMixed = true;
        }
        
        [Button]
        public Tween StartColorChangeTween(ChangePouringLiquidColorPair _changePouringLiquidColorPair)
        {
            m_StartColor = m_PouringLiquidRenderer.material.color;
            return SetColorTween(_changePouringLiquidColorPair.ChangeLiquidDuration).
                SetEase(_changePouringLiquidColorPair.ChangeLiquidEase);
        }

        #region Tween

        private Tween m_SetColorTween;

        private Tween SetColorTween(float _duration)
        {
            m_SetColorTween?.Kill();
            m_SetColorTween =  DOTween.To(() => 0.0f,
                _value => SetColorByLerp(_value),
                1.0f,
                _duration);
            return m_SetColorTween;
        }

        private Color m_CurrentColor;
        private void SetColorByLerp(float _lerp)
        {
            m_CurrentColor = Color.Lerp(m_StartColor, m_TargetColor, _lerp);
            m_PouringLiquidRenderer.material.color = m_CurrentColor;
        }

        private void KillAllTween()
        {
            m_SetColorTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
        }
    }
}