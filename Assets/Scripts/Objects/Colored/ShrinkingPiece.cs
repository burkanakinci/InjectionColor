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
                m_RemainingShrinkDuration = m_ShrinkingPieceData.OnShrinkingDuration - m_FirstShrinkDuration - m_TempDilationDuration;
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
                m_FirstShrinkDuration = 100.0f - m_TempDilationStartValue ;
                m_FirstShrinkDuration /= 100.0f;
                m_FirstShrinkDuration *= m_ShrinkingPieceData.OnDilationDuration;
                m_RemainingShrinkDuration = m_ShrinkingPieceData.OnDilationDuration - m_FirstShrinkDuration - m_TempDilationDuration;
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
            ShrinkingDelayCallTween(m_ShrinkingPieceData.OnShrinkingStartDelay, () =>
            {
                ShrinkObject();
            });
        }

        public void Dilation()
        {
            ShrinkingDelayCallTween(m_ShrinkingPieceData.OnDilationStartDelay, () =>
            {
                ChangeColorful();
                DilationObject();
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

        private Tween m_ShrinkingDelayCallTween;
        private Tween m_ShrinhkingBlendShapeTween;
        private float m_StartShrinkingBlendShapeValue;

        private Tween ShrinkingBlendShapeTween(float _target, float _duration)
        {
            m_StartShrinkingBlendShapeValue =m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
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

        public Tween ShrinkingDelayCallTween(float _delay,Action _onComplete)
        {
            m_ShrinkingDelayCallTween?.Kill();
            m_ShrinkingDelayCallTween = DOVirtual.DelayedCall(_delay,
                () =>
                {
                    _onComplete?.Invoke();
                });
            return m_ShrinkingDelayCallTween;
        }

        public void KillAllTween()
        {
            m_ShrinkingDelayCallTween?.Kill();
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ChangeMaterialTween?.Kill();
        }

        #endregion
    }
}