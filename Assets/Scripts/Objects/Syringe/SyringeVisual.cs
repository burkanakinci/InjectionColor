using System;
using DG.Tweening;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class SyringeVisual : CustomBehaviour<Syringe>
    {
        [SerializeField] private Transform m_SyringeUpperParent;
        [SerializeField] private MeshRenderer m_SyringeLiquidMaterial;
        [SerializeField] private ParticleSystem m_ColoredSplashVFX;

        public override void Initialize(Syringe _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_SyringeUpperParent.localPosition = Vector3.zero;
            m_SyringeLiquidMaterial.material.SetFloat(SyringeLiquidMaterial.FULNESS,0.0f);
            m_ColoredSplashVFXMain = m_ColoredSplashVFX.main;
            m_ColoredSplashVFXCOL = m_ColoredSplashVFX.colorOverLifetime;
            m_ColoredSplasfVFXTrail = m_ColoredSplashVFX.trails;
            
            m_ColoredSplashColorKey = new GradientColorKey[] { new GradientColorKey(), new GradientColorKey()};
            m_ColoredSplashAlphaKey = new GradientAlphaKey[] { new GradientAlphaKey(), new GradientAlphaKey() };
            m_ColoredSplashAlphaKey[0].alpha = 1.0f;
            m_ColoredSplashAlphaKey[0].time = 0.0f; 
            m_ColoredSplashAlphaKey[1].alpha = 0.0f;
            m_ColoredSplashAlphaKey[1].time = 1.0f;
        }

        public void StartDeinjectShaking(DeinjectShakingPair _deinjectShakingPair,
            DeinjectShakingBackPair _deinjectShakingBackPair)
        {
            ShakeRotationTween(_deinjectShakingPair)
                .SetEase(_deinjectShakingPair.OnDeinjectShakeEase)
                .OnComplete(() =>
                {
                    LocalRotateTween(Vector3.zero, _deinjectShakingBackPair.OnDeinjectShakeBackDuration)
                        .SetEase(_deinjectShakingBackPair.OnDeinjectShakeBackEase);
                });
        }

        public void FlipSyringe(MovementToColoredFlipPair _flipPair)
        {
            m_StartFlipAngleX = transform.localEulerAngles.x;
            FlipTween(_flipPair.OnMoveToColoredFlipDuration)
                .SetEase(_flipPair.OnMoveToColoredFlipEase);
        }

        public Tween SyringeUp(DeinjectMovementUpPair _deinjectMovementUpPair)
        {
            return SyringeUpperParentLocalMove(
                Vector3.up * _deinjectMovementUpPair.MoveUpDistance,
                _deinjectMovementUpPair.MoveUpDuration
            ).SetEase(_deinjectMovementUpPair.MoveUpEase);
        }
        
        public void SyringeDown(DeinjectMovementDownPair _deinjectMovementDownPair)
        {
            SyringeUpperParentLocalMove(
                Vector3.zero,
                _deinjectMovementDownPair.MoveDownDuration
            ).SetEase(_deinjectMovementDownPair.MoveDownEase);
        }

        public void SetSyringeLiquidColor(Color _baseColor)
        {
            m_SyringeLiquidMaterial.material.SetColor(SyringeLiquidMaterial.TOP_COLOR,_baseColor);
            m_SyringeLiquidMaterial.material.SetColor(SyringeLiquidMaterial.SIDE_COLOR,_baseColor);
        }
        public Tween SyringeLiquidUp(DeinjectLiquidUpPair _liquidUpPair)
        {
            m_StartLiquidFulnessValue = m_SyringeLiquidMaterial.material.GetFloat(SyringeLiquidMaterial.FULNESS);
            return SyringeLiquidFulnessTween(1.0f,_liquidUpPair.LiquidUpDuration).SetEase(_liquidUpPair.LiquidUpEase);
        }
        public void SyringeLiquidDown(DeinjectLiquidDownPair _liquidDownPair)
        {
            m_StartLiquidFulnessValue = m_SyringeLiquidMaterial.material.GetFloat(SyringeLiquidMaterial.FULNESS);
            SyringeLiquidFulnessTween(0.0f,_liquidDownPair.LiquidDownDuration).SetEase(_liquidDownPair.LiquidDownEase);
        }

        private ParticleSystem.MainModule m_ColoredSplashVFXMain;
        private ParticleSystem.ColorOverLifetimeModule m_ColoredSplashVFXCOL;
        private ParticleSystem.TrailModule m_ColoredSplasfVFXTrail;
        [SerializeField]private Gradient m_ColoredSplashGradient;
        private GradientColorKey[] m_ColoredSplashColorKey;
        private GradientAlphaKey[] m_ColoredSplashAlphaKey;
        [Button]
        public void SetSplashVFXColor(Color _color)
        {
            m_ColoredSplashVFXMain.startColor = _color;
            m_ColoredSplashColorKey[0].color = _color;
            m_ColoredSplashColorKey[1].color = _color;
            m_ColoredSplashGradient.SetKeys(m_ColoredSplashColorKey,m_ColoredSplashAlphaKey);
            m_ColoredSplashVFXCOL.color = m_ColoredSplashGradient;
            m_ColoredSplasfVFXTrail.colorOverLifetime = m_ColoredSplashGradient;
            m_ColoredSplasfVFXTrail.colorOverTrail = m_ColoredSplashGradient;
        }

        public void SetSplashVFXEnabled(bool _isEnable)
        {
            m_ColoredSplashVFX.gameObject.SetActive(_isEnable);
            if (_isEnable)
            {
                m_ColoredSplashVFX.Play();
            }
            else
            {
                m_ColoredSplashVFX.Stop();
            }
        }

        #region Tweens

        private Tween m_RotateTween;
        private Tween m_UpperMovementTween;
        private Tween m_LiquidFulnessTween;

        private Tween ShakeRotationTween(DeinjectShakingPair _deinjectShakingPair)
        {
            m_RotateTween?.Kill();
            m_RotateTween = transform.DOShakeRotation(
                _deinjectShakingPair.m_OnDeinjectShakeDuration,
                _deinjectShakingPair.m_OnDeinjectShakeStrength,
                _deinjectShakingPair.m_OnDeinjectShakeVibration,
                _deinjectShakingPair.m_OnDeinjectShakeRandomness,
                _deinjectShakingPair.OnDeinjectShakeIsFadeOut
            );
            return m_RotateTween;
        }

        private float m_StartFlipAngleX;

        private Tween FlipTween(float _duration)
        {
            m_RotateTween?.Kill();
            m_RotateTween = DOTween.To(() => m_StartFlipAngleX,
                _value => transform.localRotation = Quaternion.Euler(_value * Vector3.right), 360.0f, _duration);
            return m_RotateTween;
        }

        private Tween LocalRotateTween(Vector3 _target, float _duration)
        {
            m_RotateTween?.Kill();
            m_RotateTween = transform.DOLocalRotateQuaternion(Quaternion.Euler(_target), _duration);
            return m_RotateTween;
        }

        private Tween SyringeUpperParentLocalMove(Vector3 _target,float _duration)
        {
            m_UpperMovementTween?.Kill();
            m_UpperMovementTween = m_SyringeUpperParent.DOLocalMove(_target,_duration);
            return m_UpperMovementTween;
        }

        private float m_StartLiquidFulnessValue;
        private Tween SyringeLiquidFulnessTween(float _target, float _duration)
        {
            m_LiquidFulnessTween?.Kill();
            m_LiquidFulnessTween = DOTween.To(() => m_StartLiquidFulnessValue,
                _value =>SetLiquidFulness(_value) ,
                _target,
                _duration);
            return m_LiquidFulnessTween;
        }

        private void SetLiquidFulness(float _fulness)
        {
            m_SyringeLiquidMaterial.material.SetFloat(SyringeLiquidMaterial.FULNESS,_fulness);
        }

        public void KillAllTween()
        {
            m_LiquidFulnessTween?.Kill();
            m_UpperMovementTween?.Kill();
            m_RotateTween?.Kill();
        }

        #endregion
    }
}