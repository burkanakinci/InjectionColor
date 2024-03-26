using UnityEngine;

namespace Game.Object
{
    public class ColoredVisual : CustomBehaviour<Colored>
    {
        [SerializeField] private ShrinkingPiece[] m_ShrinkingPiece;
        [SerializeField] private SyringeSplashVFX m_SyringeSplashVFX;
        [SerializeField] private SyringeFirstSplashVFX m_SyringeFirstSplashVFX;

        public override void Initialize(Colored _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_SyringeSplashVFX.Initialize();
            m_SyringeFirstSplashVFX.Initialize();
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].Initialize(this);
            }

            m_ShrinkingPiece[0].OnChangeShrinkingValue += CachedComponent.OnChangeShrinkingValue;
        }
        public void DeinjectColoredVisual()
        {
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].Shrink();
            }
        }

        public void DilationColoredVisual()
        {
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].Dilation();
            }
        }

        public void SetSplashVFXEnabled(OnDisabledSplashVFXPair _disabledPair)
        {
            m_SyringeSplashVFX.SetVFXEnabled(_disabledPair);
        }
        public void SetSplashVFXEnabled(OnEnabledSplashVFXPair _enabledPair)
        {
            m_SyringeSplashVFX.SetVFXEnabled(_enabledPair);
        }

        public void SetFirstSplashVFXEnabled(bool _isEnable)
        {
            m_SyringeFirstSplashVFX.SetSplashVFXEnabled(_isEnable);
        }

        private void OnDisable()
        {
            m_ShrinkingPiece[0].OnChangeShrinkingValue -= CachedComponent.OnChangeShrinkingValue;
        }
    }
}