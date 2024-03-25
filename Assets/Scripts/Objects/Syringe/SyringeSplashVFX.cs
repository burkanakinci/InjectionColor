using UnityEngine;

namespace Game.Object
{
    public class SyringeSplashVFX : CustomBehaviour<SyringeVisual>
    {
        [SerializeField] private ParticleSystem m_ColoredSplashVFX;
        private ParticleSystem.MainModule m_ColoredSplashVFXMain;
        private ParticleSystem.ColorOverLifetimeModule m_ColoredSplashVFXCOL;
        private ParticleSystem.TrailModule m_ColoredSplasfVFXTrail;
        private Gradient m_ColoredSplashGradient;
        private GradientColorKey[] m_ColoredSplashColorKey;
        private GradientAlphaKey[] m_ColoredSplashAlphaKey;

        public override void Initialize(SyringeVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
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

        public void SetVFXColor(Color _color)
        {
            return;
            m_ColoredSplashVFXMain.startColor = _color;
            m_ColoredSplashColorKey[0].color = _color;
            m_ColoredSplashColorKey[1].color = _color;
            m_ColoredSplashGradient.SetKeys(m_ColoredSplashColorKey,m_ColoredSplashAlphaKey);
            m_ColoredSplashVFXCOL.color = m_ColoredSplashGradient;
            m_ColoredSplasfVFXTrail.colorOverLifetime = m_ColoredSplashGradient;
            m_ColoredSplasfVFXTrail.colorOverTrail = m_ColoredSplashGradient;
        }

        public void SetVFXEnabled(bool _isEnable)
        {
            return;
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
    }
}