using DG.Tweening;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupLiquid : CustomBehaviour<PouringCupVisual>
    {
        [SerializeField] private MeshRenderer m_PouringLiquidRenderer;
        private int m_MixedCount;
        
        public override void Initialize(PouringCupVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_MixedCount = 0;
        }

        private Color m_AddedColor;
        private Color m_TargetColor;
        private Color m_StartColor;
        private float m_TargetColorMultiply;
        [Button]
        public void SetTargetColorOnDeinject(Color _addedColor)
        {
            ++m_MixedCount;
            m_AddedColor = _addedColor;
            m_AddedColor.r /= m_MixedCount;
            m_AddedColor.g /= m_MixedCount;
            m_AddedColor.b /= m_MixedCount;
            m_AddedColor.a = 1.0f;

            m_TargetColorMultiply = m_MixedCount - 1;
            m_TargetColorMultiply /= m_MixedCount;
            m_TargetColor.r *= m_TargetColorMultiply;
            m_TargetColor.g *= m_TargetColorMultiply;
            m_TargetColor.b *= m_TargetColorMultiply;
            
            m_TargetColor.r += m_AddedColor.r;
            m_TargetColor.g += m_AddedColor.g;
            m_TargetColor.b += m_AddedColor.b;
            m_TargetColor.a = 1.0f;
        }
        
        [Button]
        public void StartColorChangeTween(ChangePouringLiquidColorPair _changePouringLiquidColorPair)
        {
            m_StartColor = m_PouringLiquidRenderer.material.GetColor(PouringLiquidMaterial.BASE_COLOR);
            SetColorTween(_changePouringLiquidColorPair.ChangeLiquidDuration).
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
            m_PouringLiquidRenderer.material.SetColor(PouringLiquidMaterial.BASE_COLOR,m_CurrentColor); 
        }

        private void KillAllTween()
        {
            m_SetColorTween?.Kill();
        }

        #endregion
    }
}