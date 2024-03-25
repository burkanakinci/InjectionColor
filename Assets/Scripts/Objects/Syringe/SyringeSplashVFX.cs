using UnityEngine;

namespace Game.Object
{
    public class SyringeSplashVFX : CustomBehaviour
    {
        [SerializeField] private ParticleSystem m_ColoredSplashVFX;

        public override void Initialize()
        {
            SetVFXEnabled(false);
        }

        public void SetVFXEnabled(bool _isEnable)
        {
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