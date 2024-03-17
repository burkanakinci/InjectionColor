using DG.Tweening;
using UnityEngine;

namespace Game.Object
{
    public class ShrinkingPiece : CustomBehaviour<ColoredVisual>
    {
        [SerializeField] private SkinnedMeshRenderer m_ShrinkingPieceRenderer;
        [SerializeField] private ShrinkingPieceData m_ShrinkingPieceData;

        public void ShrinkObject()
        {
            m_StartShrinkingBlendShapeValue = m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
            ShrinkingBlendShapeTween(100.0f,m_ShrinkingPieceData.OnShrinkingDuration).SetEase(m_ShrinkingPieceData.OnShrinkingEase);
        }

        private Tween m_ShrinhkingBlendShapeTween;
        private float m_StartShrinkingBlendShapeValue;

        private Tween ShrinkingBlendShapeTween(float _target,float _duration)
        {
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ShrinhkingBlendShapeTween = DOTween.To(() => m_StartShrinkingBlendShapeValue,
                _value => m_ShrinkingPieceRenderer.SetBlendShapeWeight(0, _value), _target, _duration);
            return m_ShrinhkingBlendShapeTween;
        }

        public void KillAllTween()
        {
            m_ShrinhkingBlendShapeTween?.Kill();
        }
    }
}