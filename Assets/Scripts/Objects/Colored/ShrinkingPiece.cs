using System;
using DG.Tweening;
using Game.Manager;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class ShrinkingPiece : CustomBehaviour<ColoredVisual>
    {
        [SerializeField] private SkinnedMeshRenderer m_ShrinkingPieceRenderer;
        [SerializeField] private ShrinkingPieceData m_ShrinkingPieceData;
        [SerializeField]private Material m_StartMaterial;
        private Entities m_Entities;

        public override void Initialize(ColoredVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_Entities = GameManager.Instance.GetManager<Entities>();
        }

        public void ShrinkObject()
        {
            float _tempDilationValue = m_ShrinkingPieceData.OnShrinkingBackDilationStartValue;
            m_StartShrinkingBlendShapeValue =m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
            ShrinkingBlendShapeTween(100.0f,
                    m_ShrinkingPieceData.OnShrinkingDuration)
                .SetEase(m_ShrinkingPieceData.OnShrinkingEase)
                .OnUpdate(() =>
                {
                    if (m_ShrinkingPieceRenderer.GetBlendShapeWeight(0) >= _tempDilationValue &&
                        m_ShrinkingPieceData.OnShrinkingUseBackDilation)
                    {
                        ShrinkingBlendShapeTween(
                                m_ShrinkingPieceRenderer.GetBlendShapeWeight(0) -
                                m_ShrinkingPieceData.OnShrinkingBackDilationValue,
                                m_ShrinkingPieceData.OnShrinkingBackDilationDuration)
                            .SetEase(m_ShrinkingPieceData.OnShrinkingBackDilationEase)
                            .OnComplete(() =>
                            {
                                float _remainingShrinkDuration =
                                    (100.0f - m_ShrinkingPieceRenderer.GetBlendShapeWeight(0)) * 0.01f;
                                _remainingShrinkDuration =
                                    m_ShrinkingPieceData.OnShrinkingDuration * _remainingShrinkDuration;
                                ShrinkingBlendShapeTween(100.0f,
                                        _remainingShrinkDuration)
                                    .SetEase(m_ShrinkingPieceData.OnShrinkingEase);
                            });
                    }
                });
        }

        private void DilationObject()
        {
            float _tempDilationValue = m_ShrinkingPieceData.OnDilationBackDilationStartValue;
            m_StartShrinkingBlendShapeValue =m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
            ShrinkingBlendShapeTween(0.0f,
                    m_ShrinkingPieceData.OnDilationDuration)
                .SetEase(m_ShrinkingPieceData.OnDilationEase)
                .OnUpdate(() =>
                {
                    if (m_ShrinkingPieceRenderer.GetBlendShapeWeight(0) >= _tempDilationValue &&
                        m_ShrinkingPieceData.OnDilationUseBackDilation)
                    {
                        ShrinkingBlendShapeTween(
                                m_ShrinkingPieceRenderer.GetBlendShapeWeight(0) -
                                m_ShrinkingPieceData.OnDilationBackDilationValue,
                                m_ShrinkingPieceData.OnDilationBackDilationDuration)
                            .SetEase(m_ShrinkingPieceData.OnDilationBackDilationEase)
                            .OnComplete(() =>
                            {
                                float _remainingShrinkDuration =
                                    (100.0f - m_ShrinkingPieceRenderer.GetBlendShapeWeight(0)) * 0.01f;
                                _remainingShrinkDuration =
                                    m_ShrinkingPieceData.OnDilationDuration * _remainingShrinkDuration;
                                ShrinkingBlendShapeTween(100.0f,
                                        _remainingShrinkDuration)
                                    .SetEase(m_ShrinkingPieceData.OnDilationEase);
                            });
                    }
                });
        }

        public void ChangeColorless()
        {
            m_StartTweenMaterial = m_StartMaterial;
            m_TargetTweenMaterial = m_Entities.GetColoredObjectsMaterial(ColoredObjectMaterialType.Colorless);
                ChangeMaterialTween(m_ShrinkingPieceData.OnChangeColorlessDuration)
                .SetEase(m_ShrinkingPieceData.OnChangeColorlessEase);
        }

        public void ChangeColorful()
        {
            m_StartTweenMaterial = m_Entities.GetColoredObjectsMaterial(ColoredObjectMaterialType.Colorless);
            m_TargetTweenMaterial = m_StartMaterial;
            ChangeMaterialTween(m_ShrinkingPieceData.OnChangeColofulDuration)
                .SetEase(m_ShrinkingPieceData.OnChangeColofulEase);
        }

        #region Tweens

        private Tween m_DilationDelayCallTween;
        private Tween m_ShrinhkingBlendShapeTween;
        private float m_StartShrinkingBlendShapeValue;

        private Tween ShrinkingBlendShapeTween(float _target, float _duration)
        {
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ShrinhkingBlendShapeTween = DOTween.To(() => m_StartShrinkingBlendShapeValue,
                _value =>SetShrinkBlendShape(_value) ,
                _target,
                _duration);
            return m_ShrinhkingBlendShapeTween;
        }

        private void SetShrinkBlendShape(float _shapeValue)
        {
            m_ShrinkingPieceRenderer.SetBlendShapeWeight(0, _shapeValue);
        }

        private Tween m_ChangeMaterialTween;
        private Material m_StartTweenMaterial;
        private Material m_TargetTweenMaterial;

        private Tween ChangeMaterialTween(float _duration)
        {
            m_ChangeMaterialTween?.Kill();
            m_ChangeMaterialTween = DOTween.To(() => 0.0f,
                _value => ChangeMaterialByLerp(_value),
                1.0f,
                _duration);
            return m_ChangeMaterialTween;
        }

        private void ChangeMaterialByLerp(float _lerp)
        {
            m_ShrinkingPieceRenderer.material.Lerp(m_StartTweenMaterial, m_TargetTweenMaterial, _lerp);
        }

        public Tween DilationDelayCallTween()
        {
            m_DilationDelayCallTween?.Kill();
            m_DilationDelayCallTween = DOVirtual.DelayedCall(m_ShrinkingPieceData.OnDilationStartDelay,
                () =>
                {
                    ChangeColorful();
                    DilationObject();
                });
            return m_DilationDelayCallTween;
        }

        public void KillAllTween()
        {
            m_DilationDelayCallTween?.Kill();
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ChangeMaterialTween?.Kill();
        }

        #endregion
    }
}