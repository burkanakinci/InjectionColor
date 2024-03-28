using System;
using DG.Tweening;
using Game.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class Syringe : CustomBehaviour
    {
        [SerializeField] private SyringeVisual m_SyringeVisual;
        [SerializeField] private SyringeData m_SyringeData;
        private PouringCup m_PouringCup;

        public override void Initialize()
        {
            m_SyringeVisual.Initialize(this);
            m_CurrentCamera = GameManager.Instance.GetManager<CameraManager>().CurrentCamera;
            m_PouringCup = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup;
            transform.position = m_PouringCup.SyringePouringParent.position;
        }

        private Colored m_CurrentColored;
        private Camera m_CurrentCamera;

        public void StartInjectColored(Colored _colored)
        {
            m_CurrentColored = _colored;
            transform.SetParent(_colored.SyringeTargetParent);
            LocalJumpTween(Vector3.up * m_SyringeData.OnMoveToColoredJumpFirstHeight, 
                    m_SyringeData.OnMoveToColoredJumpPower + transform.localPosition.y,
                    m_SyringeData.OnMoveToColoredJumpDuration)
                .SetEase(m_SyringeData.OnMoveToColoredJumpEase)
                .OnComplete(() =>
                {
                    LocalMoveTween(Vector3.zero,m_SyringeData.OnMoveToColoredSettleDuration)
                        .SetEase(m_SyringeData.OnMoveToColoredSettleEase)
                        .OnComplete(() =>
                        {
                            _colored.DeinjectColor();
                            m_SyringeVisual.SetSyringeLiquidColor(_colored.ObjectColor);
                            m_SyringeVisual.StartDeinjectShaking(m_SyringeData.DeinjectShakingPair,m_SyringeData.DeinjectShakingBackPair);
                            m_SyringeVisual.SyringeUp(m_SyringeData.DeinjectMovementUpPair);
                            m_SyringeVisual.SyringeLiquidUp(m_SyringeData.DeinjectLiquidUpPair)
                                .OnComplete(() =>
                                {
                                    CompleteColoredInject();
                                });
                        });
                });
            LocalRotateTween(Vector3.zero,m_SyringeData.OnMoveToColoredRotateDuration)
                .SetEase(m_SyringeData.OnMoveToColoredRotateEase);
        }

        private void CompleteColoredInject()
        {
            JumpDelayedTween(m_SyringeData.OnSyringePourMovementStartDelay,
                () =>
                {
                    transform.SetParent(m_PouringCup.SyringePouringParent);
                    m_CurrentColored.SetSplashVFXEnabled(false);
                    LocalJumpTween(
                            Vector3.zero,
                            m_SyringeData.OnSyringePourMovementJumpPower + transform.localPosition.y,
                            m_SyringeData.OnSyringePourMovementJumpDuration)
                        .SetEase(m_SyringeData.OnSyringePourMovementJumpEase)
                        .OnComplete(() =>
                        {
                            m_CurrentColored.DilationColored();
                            CompleteSyringePourJump();
                        });
                    LocalRotateTween(Vector3.zero,m_SyringeData.OnSyringePourMovementRotateDuration)
                        .SetEase(m_SyringeData.OnSyringePourMovementRotateEase);
                });
        }

        private void CompleteSyringePourJump()
        {
            SyringeUpperMovementDownDelayCall(m_SyringeData.DeinjectMovementDownPair.MoveDownStartDelay,
                () =>
                {
                    m_SyringeVisual.SyringeDown(m_SyringeData.DeinjectMovementDownPair);
                    m_SyringeVisual.SyringeLiquidDown(m_SyringeData.DeinjectLiquidDownPair);
                    m_PouringCup.SetColorOnDeinject(m_CurrentColored.ObjectColor);
                });
        }

        #region Tween

        private Tween m_MoveTween;
        private Tween m_RotateTween;
        private Tween m_JumpDelayedTween;
        private Tween m_SyringeUpperMovementDownDelayCall;

        private Tween MoveTween(Vector3 _target, float _duration)
        {
            m_MoveTween?.Kill();
            m_MoveTween = transform.DOMove(_target, _duration);
            return m_MoveTween;
        }

        private Tween LocalJumpTween(Vector3 _target, float _power, float _duration)
        {
            m_MoveTween?.Kill();
            m_MoveTween = transform.DOLocalJump(_target, _power, 1, _duration);
            return m_MoveTween;
        }
        
        private Tween LocalMoveTween(Vector3 _target, float _duration)
        {
            m_MoveTween?.Kill();
            m_MoveTween = transform.DOLocalMove(_target, _duration);
            return m_MoveTween;
        }

        private Tween LocalRotateTween(Vector3 _target, float _duration)
        {
            m_RotateTween?.Kill();
            m_RotateTween = transform.DORotateQuaternion(Quaternion.Euler(_target),_duration);
            return m_RotateTween;
        }

        private void JumpDelayedTween(float _delay,Action _jumpTween)
        {
            m_JumpDelayedTween?.Kill();
            m_JumpDelayedTween = DOVirtual.DelayedCall(_delay, () =>
            {
                _jumpTween?.Invoke();
            });
        }
        private void SyringeUpperMovementDownDelayCall(float _delay, Action _onComplete)
        {
            m_SyringeUpperMovementDownDelayCall?.Kill();
            m_SyringeUpperMovementDownDelayCall = DOVirtual.DelayedCall(_delay, () =>
            {
                _onComplete?.Invoke();
            });

        }

        public void KillAllTween()
        {
            m_SyringeUpperMovementDownDelayCall?.Kill();
            m_JumpDelayedTween?.Kill();
            m_SyringeVisual.KillAllTween();
            m_MoveTween?.Kill();
            m_RotateTween?.Kill();
        }

        #endregion

        private void OnDisable()
        {
            KillAllTween();
        }
    }
}