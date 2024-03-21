using DG.Tweening;
using Game.Utilities.Constants;
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
        public void SetTargetColorOnDeinject(Color _addedColor)
        {
            ++m_MixedCount;
            m_AddedColor = _addedColor;
            m_AddedColor /= m_MixedCount;
            m_TargetColor *= ((m_MixedCount - 1) / m_MixedCount);
            m_TargetColor += m_AddedColor;
        }

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