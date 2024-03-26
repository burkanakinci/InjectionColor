using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class SyringeSplashVFX : CustomBehaviour
    {
        [SerializeField] private ParticleSystem m_ColoredSplashVFX;

        public override void Initialize()
        {
            m_ColoredSplashVFX.Stop();
            m_VFXMain = m_ColoredSplashVFX.main;
            m_VFXMain.startSpeed = 0.0f;
        }

        public void SetVFXEnabled(OnDisabledSplashVFXPair _disabledPair)
        {
            EnabledTween(0.0f, _disabledPair.DisabledSimulationDuration)
                .SetEase(_disabledPair.DisabledSimulationEase)
                .OnComplete(() =>
                {
                    m_ColoredSplashVFX.gameObject.SetActive(false);
                    m_ColoredSplashVFX.Stop();
                });
        }
        public void SetVFXEnabled(OnEnabledSplashVFXPair _enabledPair)
        {
            m_ColoredSplashVFX.gameObject.SetActive(true);
            m_ColoredSplashVFX.Play();
            EnabledTween(_enabledPair.EnabledSimulationSpeed, _enabledPair.EnabledSimulationDuration)
                .SetEase(_enabledPair.EnabledSimulationEase);
        }
        #region Tween

        private Tween m_EnabledTween;
        private ParticleSystem.MinMaxCurve m_StartSpeed;
        private ParticleSystem.MinMaxCurve m_TargetSpeed;
        private ParticleSystem.MainModule m_VFXMain;
        [Button]
        private Tween EnabledTween(float _targetSpeed,float _duration)
        {
            m_TargetSpeed.constant = _targetSpeed;
            m_StartSpeed.constant = m_VFXMain.startSpeed.constant;
            m_EnabledTween?.Kill();
            m_EnabledTween = DOTween.To(()=>
                0.0f,
                _value => SetVFXSpeed(_value),
                1.0f,
                _duration
                );
            return m_EnabledTween;
        }

        private void SetVFXSpeed(float _lerp)
        {
            m_VFXMain.startSpeed = Mathf.Lerp(m_StartSpeed.constant,m_TargetSpeed.constant,_lerp);
        }

        public void KillAllTween()
        {
            m_EnabledTween?.Kill();
        }

        #endregion
    }
}