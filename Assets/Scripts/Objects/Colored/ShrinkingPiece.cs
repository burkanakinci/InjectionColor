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
        private Entities m_Entities;
        public override void Initialize(ColoredVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_Entities = GameManager.Instance.GetManager<Entities>();
        }

        private void OnEnable()
        {
            Debug.Log("ENABLED");
        }

        private void OnDisable()
        {
            Debug.Log("DISABLED");
        }

        public void ShrinkObject()
        {
            m_StartShrinkingBlendShapeValue = m_ShrinkingPieceRenderer.GetBlendShapeWeight(0);
            ShrinkingBlendShapeTween(100.0f,m_ShrinkingPieceData.OnShrinkingDuration).SetEase(m_ShrinkingPieceData.OnShrinkingEase);
        }

        public void ChangeColorless()
        {
            ChangeMaterialTween(m_Entities.GetColoredObjectsMaterial(ColoredObjectMaterialType.Colorless),m_ShrinkingPieceData.OnChangeColorlessDuration)
                .SetEase(m_ShrinkingPieceData.OnChangeColorlessEase);
        }

        #region Tweens

        private Tween m_ShrinhkingBlendShapeTween;
        private float m_StartShrinkingBlendShapeValue;

        private Tween ShrinkingBlendShapeTween(float _target,float _duration)
        {
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ShrinhkingBlendShapeTween = DOTween.To(() => m_StartShrinkingBlendShapeValue,
                _value => m_ShrinkingPieceRenderer.SetBlendShapeWeight(0, _value), _target, _duration);
            return m_ShrinhkingBlendShapeTween;
        }

        private Tween m_ChangeMaterialTween;
        private Material m_StartMaterial;
        private Material m_TargetMaterial;

        private Tween ChangeMaterialTween(Material _target,float _duration)
        {
            m_ChangeMaterialTween?.Kill();
            m_StartMaterial = m_ShrinkingPieceRenderer.material;
            m_TargetMaterial = _target;
            m_ChangeMaterialTween = DOTween.To(() => 0.0f,
                _value => ChangeMaterialByLerp(_value),
                1.0f,
                _duration);
            return m_ChangeMaterialTween;
        }

        private void ChangeMaterialByLerp(float _lerp)
        {
            m_ShrinkingPieceRenderer.material.Lerp(m_StartMaterial, m_TargetMaterial, _lerp);
        }

        public void KillAllTween()
        {
            m_ShrinhkingBlendShapeTween?.Kill();
            m_ChangeMaterialTween?.Kill();
        }

        #endregion
    }
}