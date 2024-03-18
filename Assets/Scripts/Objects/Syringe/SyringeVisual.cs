using DG.Tweening;
using UnityEngine;

namespace Game.Object
{
    public class SyringeVisual : CustomBehaviour<Syringe>
    {
        public override void Initialize(Syringe _cachedComponent)
        {
            base.Initialize(_cachedComponent);
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

        #region Tweens

        private Tween m_RotateTween;

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

        public void KillAllTween()
        {
            m_RotateTween?.Kill();
        }

        #endregion
    }
}