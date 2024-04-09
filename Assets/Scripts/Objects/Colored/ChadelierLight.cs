using DG.Tweening;
using Game.Manager;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.Object
{
    public class ChadelierLight : ShrinkingPiece
    {
        private MeshRenderer m_LightRenderer;
        protected override void InitializeShrinkingObject()
        {
            m_LightRenderer = GetComponent<MeshRenderer>();
            m_Entities = GameManager.Instance.GetManager<Entities>();
            m_TargetTweenMaterial = m_Entities.GetColoredObjectsMaterial(ColoredObjectMaterialType.LightColorless);
            SetShrinkBlendShape(0.0f);
        }

        private float m_CurrentShapeValue;
        protected override Tween ShrinkingBlendShapeTween(float _target, float _duration)
        {
            m_StartShrinkingBlendShapeValue = m_CurrentShapeValue;
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ShrinhkingBlendShapeTween = DOTween.To(() => m_StartShrinkingBlendShapeValue,
                _value => SetShrinkBlendShape(_value),
                _target,
                _duration);
            return m_ShrinhkingBlendShapeTween;
        }

        protected override void SetShrinkBlendShape(float _shapeValue)
        {
            m_CurrentShapeValue = _shapeValue;
            m_TempMaterialLerp = _shapeValue * 0.01f;
            m_LightRenderer.material.Lerp(m_StartMaterial, m_TargetTweenMaterial, m_TempMaterialLerp);
        }
    }
}