using System;
using DG.Tweening;
using Game.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using AudioType = Game.Manager.AudioType;

namespace Game.Object
{
    public class Syringe : CustomBehaviour
    {
        [SerializeField] private SyringeVisual m_SyringeVisual;
        [SerializeField] private SyringeData m_SyringeData;
        private PouringCup m_PouringCup;
        private AudioManager m_AudioManager;

        public override void Initialize()
        {
            m_SyringeVisual.Initialize(this);
            m_CurrentCamera = GameManager.Instance.GetManager<CameraManager>().CurrentCamera;
            m_PouringCup = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup;
            m_AudioManager = GameManager.Instance.GetManager<AudioManager>();
            transform.position = m_PouringCup.SyringeStartParent.position;
        }

        private Colored m_CurrentColored;
        private Camera m_CurrentCamera;

        public void StartInjectColored(Colored _colored)
        {
            m_AudioManager.Play(AudioType.SyringeMoveToColored);
            m_CurrentColored = _colored;
            transform.SetParent(_colored.SyringeTargetParent);
            ScaleTween(1.0f, m_SyringeData.OnMoveToColoredJumpDuration);
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
                            m_AudioManager.Play(AudioType.SyringeStab);
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
                    m_AudioManager.Play(AudioType.SyringeBackFromColored);
                    transform.SetParent(m_PouringCup.SyringePouringParent);
                    m_CurrentColored.SetSplashVFXEnabled(false);
                    ScaleTween(1.0f, m_SyringeData.OnSyringePourMovementJumpDuration);
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
                    m_AudioManager.Play(AudioType.SyringePouringLiquid);
                    m_SyringeVisual.SyringeDown(m_SyringeData.DeinjectMovementDownPair);
                    m_SyringeVisual.SyringeLiquidDown(m_SyringeData.DeinjectLiquidDownPair)
                        .OnComplete(() =>
                        {
                            OnCompleteSyringePouring();
                        });
                    m_PouringCup.SetColorOnDeinject(m_CurrentColored.ObjectColor);
                });
        }

        private void OnCompleteSyringePouring()
        {
            transform.SetParent(m_PouringCup.SyringeStartParent);
            JumpDelayedTween(m_SyringeData.OnSyringeCompletedPouringStartDelay,
                () =>
                {
                    ScaleTween(1.0f,m_SyringeData.OnSyringeCompletedPouringJumpDuration);
                    LocalJumpTween(
                            Vector3.zero,
                            m_SyringeData.OnSyringeCompletedPouringJumpPower + transform.localPosition.y,
                            m_SyringeData.OnSyringeCompletedPouringJumpDuration
                        )
                        .SetEase(m_SyringeData.OnSyringeCompletedPouringJumpEase);
                });
        }

        #region Tween

        private Tween m_MoveTween;
        private Tween m_RotateTween;
        private Tween m_JumpDelayedTween;
        private Tween m_SyringeUpperMovementDownDelayCall;
        private Tween m_ScaleTween;
        public Tween ScaleTween(float _scaleMultiply,float _duration)
        {
            m_ScaleTween?.Kill();
            m_ScaleTween = transform.DOScale(Vector3.one * _scaleMultiply , _duration);
            return m_ScaleTween;
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
            m_RotateTween = transform.DOLocalRotateQuaternion(Quaternion.Euler(_target),_duration);
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
            m_ScaleTween?.Kill();
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