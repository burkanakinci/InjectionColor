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

        private Vector3 m_ColoredTarget;
        private Colored m_CurrentColored;
        private Camera m_CurrentCamera;

        public void StartDeinjectColored(Colored _colored)
        {
            m_CurrentColored = _colored;
            m_ColoredTarget = (m_CurrentColored.SyringeTargetPos.position - m_CurrentCamera.transform.position) *
                              m_SyringeData.OnMoveToColoredTargetDistanceMultiply;
            m_ColoredTarget = m_CurrentCamera.transform.position + m_ColoredTarget;
            JumpTween(m_ColoredTarget, 
                    m_SyringeData.OnMoveToColoredJumpPower + transform.position.y,
                    m_SyringeData.OnMoveToColoredJumpDuration)
                .SetEase(m_SyringeData.OnMoveToColoredJumpEase)
                .OnComplete(() =>
                {
                    m_SyringeVisual.SetSyringeLiquidColor(_colored.ObjectColor);
                     m_SyringeVisual.SyringeUp(m_SyringeData.DeinjectMovementUpPair);
                    // m_SyringeVisual.StartDeinjectShaking(m_SyringeData.DeinjectShakingPair,m_SyringeData.DeinjectShakingBackPair);
                    // _colored.DeinjectColor();
                    // JumpDelayedTween(m_SyringeData.OnSyringePourMovementStartDelay,
                    //     () =>
                    //     {
                    //         JumpTween(
                    //                 m_PouringCup.SyringePouringParent.position,
                    //                 m_SyringeData.OnSyringePourMovementJumpPower + transform.position.y,
                    //                 m_SyringeData.OnSyringePourMovementJumpDuration)
                    //             .SetEase(m_SyringeData.OnSyringePourMovementJumpEase)
                    //             .OnComplete(() =>
                    //             {
                    //                 SyringeUpperMovementDownDelayCall(m_SyringeData.DeinjectMovementDownPair.MoveDownStartDelay,
                    //                     () =>
                    //                     {
                    //                         m_SyringeVisual.SyringeDown(m_SyringeData.DeinjectMovementDownPair);
                    //                         m_SyringeVisual.SyringeLiquidDown(m_SyringeData.DeinjectLiquidDownPair);
                    //                         m_PouringCup.SetColorOnDeinject(m_CurrentColored.ObjectColor);
                    //                     });
                    //             });
                    //     });
                });
            RotateTween(m_CurrentColored.SyringeTargetPos.eulerAngles,m_SyringeData.OnMoveToColoredRotateDuration)
                .SetEase(m_SyringeData.OnMoveToColoredRotateEase);
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

        private Tween JumpTween(Vector3 _target, float _power, float _duration)
        {
            m_MoveTween?.Kill();
            m_MoveTween = transform.DOJump(_target, _power, 1, _duration);
            return m_MoveTween;
        }

        private Tween RotateTween(Vector3 _target, float _duration)
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
    }
}