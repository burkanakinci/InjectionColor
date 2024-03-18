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

        public override void Initialize()
        {
            m_SyringeVisual.Initialize(this);
            m_CurrentCamera = GameManager.Instance.GetManager<CameraManager>().CurrentCamera;
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
                    m_SyringeVisual.StartDeinjectShaking(m_SyringeData.DeinjectShakingPair,m_SyringeData.DeinjectShakingBackPair);
                    _colored.DeinjectColor();
                });
            //m_SyringeVisual.FlipSyringe(m_SyringeData.MovementToColoredFlipPair);
            RotateTween(m_CurrentColored.SyringeTargetPos.eulerAngles,m_SyringeData.OnMoveToColoredRotateDuration)
                .SetEase(m_SyringeData.OnMoveToColoredRotateEase);
        }

        #region Tween

        private Tween m_MoveTween;
        private Tween m_RotateTween;

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

        public void KillAllTween()
        {
            m_SyringeVisual.KillAllTween();
            m_MoveTween?.Kill();
            m_RotateTween?.Kill();
        }

        #endregion
    }
}