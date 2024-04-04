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
        private SkinnedMeshRenderer m_ShrinkingPieceRenderer;
        [SerializeField] private ShrinkingPieceData m_ShrinkingPieceData; 
        [SerializeField]private Material m_StartMaterial;
        private Entities m_Entities;
        public Action<float> OnChangeShrinkingValue;

        public override void Initialize(ColoredVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_ShrinkingPieceRenderer = GetComponent<SkinnedMeshRenderer>();
            m_Entities = GameManager.Instance.GetManager<Entities>();
            m_TargetTweenMaterial = m_Entities.GetColoredObjectsMaterial(ColoredObjectMaterialType.Colorless);
            SetShrinkBlendShape(0.0f);
        }

        private float m_TempDilationStartValue;
        private float m_TempDilationDuration;
        private float m_FirstShrinkDuration;
        private float m_RemainingShrinkDuration;

        [Button]
        private void ShrinkObject()
        {
            if (m_ShrinkingPieceData.OnShrinkingUseBackDilation)
            {
                m_TempDilationStartValue = m_ShrinkingPieceData.OnShrinkingBackDilationStartValue;
                m_TempDilationDuration = m_ShrinkingPieceData.OnShrinkingBackDilationDuration;
                m_FirstShrinkDuration = m_TempDilationStartValue / 100.0f;
                m_FirstShrinkDuration *= m_ShrinkingPieceData.OnShrinkingDuration;
                m_RemainingShrinkDuration = m_ShrinkingPieceData.OnShrinkingDuration - m_FirstShrinkDuration -
                                            m_TempDilationDuration;
                ShrinkingBlendShapeTween(m_TempDilationStartValue,
                        m_FirstShrinkDuration)
                    .SetEase(m_ShrinkingPieceData.OnShrinkingBackDilationEase)
                    .OnComplete(() =>
                    {
                        ShrinkingBlendShapeTween(
                                m_TempDilationStartValue - m_ShrinkingPieceData.OnShrinkingBackDilationValue,
                                m_TempDilationDuration)
                            .SetEase(m_ShrinkingPieceData.OnShrinkingBackDilationEase)
                            .OnComplete(() =>
                            {
                                ShrinkingBlendShapeTween(100.0f,
                                        m_RemainingShrinkDuration)
                                    .SetEase(m_ShrinkingPieceData.OnShrinkingEase);
                            });
                    });
            }
            else
            {
                ShrinkingBlendShapeTween(100.0f,
                        m_ShrinkingPieceData.OnShrinkingDuration)
                    .SetEase(m_ShrinkingPieceData.OnShrinkingEase);
            }
        }

        private void DilationObject()
        {
            if (m_ShrinkingPieceData.OnDilationUseBackDilation)
            {
                m_TempDilationStartValue = m_ShrinkingPieceData.OnDilationBackDilationStartValue;
                m_TempDilationDuration = m_ShrinkingPieceData.OnDilationBackDilationDuration;
                m_FirstShrinkDuration = 100.0f - m_TempDilationStartValue;
                m_FirstShrinkDuration /= 100.0f;
                m_FirstShrinkDuration *= m_ShrinkingPieceData.OnDilationDuration;
                m_RemainingShrinkDuration = m_ShrinkingPieceData.OnDilationDuration - m_FirstShrinkDuration -
                                            m_TempDilationDuration;
                ShrinkingBlendShapeTween(m_TempDilationStartValue,
                        m_FirstShrinkDuration)
                    .SetEase(m_ShrinkingPieceData.OnDilationBackDilationEase)
                    .OnComplete(() =>
                    {
                        ShrinkingBlendShapeTween(
                                m_TempDilationStartValue + m_ShrinkingPieceData.OnDilationBackDilationValue,
                                m_TempDilationDuration)
                            .SetEase(m_ShrinkingPieceData.OnDilationBackDilationEase)
                            .OnComplete(() =>
                            {
                                ShrinkingBlendShapeTween(0.0f,
                                        m_RemainingShrinkDuration)
                                    .SetEase(m_ShrinkingPieceData.OnDilationEase);
                            });
                    });
            }
            else
            {
                ShrinkingBlendShapeTween(0.0f,
                        m_ShrinkingPieceData.OnShrinkingDuration)
                    .SetEase(m_ShrinkingPieceData.OnShrinkingEase);
            }
        }

        public void Shrink()
        {
            ShrinkingDelayCallTween(m_ShrinkingPieceData.OnShrinkingStartDelay, () => { ShrinkObject(); });
        }

        public void Dilation()
        {
            ShrinkingDelayCallTween(m_ShrinkingPieceData.OnDilationStartDelay, () =>
            {
                DilationObject();
            });
        }

        #region Tweens

        private Tween m_ShrinkingDelayCallTween;
        private Tween m_ShrinhkingBlendShapeTween;
        private float m_StartShrinkingBlendShapeValue;

        private Tween ShrinkingBlendShapeTween(float _target, float _duration)
        {
            m_StartShrinkingBlendShapeValue = m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ShrinhkingBlendShapeTween = DOTween.To(() => m_StartShrinkingBlendShapeValue,
                _value => SetShrinkBlendShape(_value),
                _target,
                _duration);
            return m_ShrinhkingBlendShapeTween;
        }
        
        private float m_TempMaterialLerp;
        private void SetShrinkBlendShape(float _shapeValue)
        {
            m_ShrinkingPieceRenderer.SetBlendShapeWeight(0, _shapeValue);
            m_TempMaterialLerp = _shapeValue * 0.01f;
            m_ShrinkingPieceRenderer.material.Lerp(m_StartMaterial, m_TargetTweenMaterial, m_TempMaterialLerp);
            OnChangeShrinkingValue?.Invoke(m_TempMaterialLerp);
        }

        private Tween m_ChangeMaterialTween;
        private Material m_StartTweenMaterial;
        private Material m_TargetTweenMaterial;

        public Tween ShrinkingDelayCallTween(float _delay, Action _onComplete)
        {
            m_ShrinkingDelayCallTween?.Kill();
            m_ShrinkingDelayCallTween = DOVirtual.DelayedCall(_delay,
                () => { _onComplete?.Invoke(); });
            return m_ShrinkingDelayCallTween;
        }

        public void KillAllTween()
        {
            m_ShrinkingDelayCallTween?.Kill();
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ChangeMaterialTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
        }
    }
}