
using System.Collections;
using UnityEngine;

namespace Game.Object
{
    public class SyringeFirstSplashVFX : CustomBehaviour
    {
        [SerializeField] private ParticleSystem m_SplashVFX;

        public override void Initialize()
        {
            SetSplashVFXEnabled(false);
        }

        public void SetSplashVFXEnabled(bool _isEnable)
        {
            m_SplashVFX.gameObject.SetActive(_isEnable);
            if (_isEnable)
            {
                m_SplashVFX.Play();
                StartParticleIsAliveCoroutine();
            }
            else
            {
                m_SplashVFX.Stop();
            }
        }
        private Coroutine m_ParticleIsAliveCoroutine;
        private void StartParticleIsAliveCoroutine()
        {
            if (m_ParticleIsAliveCoroutine != null)
            {
                StopCoroutine(m_ParticleIsAliveCoroutine);
            }

            m_ParticleIsAliveCoroutine = StartCoroutine(ParticleIsAlive());
        }
        private IEnumerator ParticleIsAlive()
        {
            yield return new WaitUntil(() => (!m_SplashVFX.IsAlive()));
            SetSplashVFXEnabled(false);
        }
    }
}
