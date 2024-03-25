using UnityEngine;

namespace Game.Object
{
    public class PouringCupVFX : CustomBehaviour<PouringCupVisual>
    {
        [SerializeField] private ParticleSystem m_PouringCupVFX;

        public override void Initialize(PouringCupVisual _cachedComponent)
        {
            base.Initialize(_cachedComponent);
        }

        public void SetPouringCupVFXEnabled(bool _isEnable)
        {
            m_PouringCupVFX.gameObject.SetActive(_isEnable);
            if (_isEnable)
            {
                m_PouringCupVFX.Play();
            }
            else
            {
                m_PouringCupVFX.Stop();
            }
        }
    }
}
