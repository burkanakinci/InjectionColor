using UnityEngine;

namespace Game.Object
{
    public class ColoredVisual : CustomBehaviour<Colored>
    {
        [SerializeField] private ShrinkingPiece[] m_ShrinkingPiece;
        [SerializeField] private SyringeSplashVFX m_SyringeSplashVFX;

        public override void Initialize(Colored _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_SyringeSplashVFX.Initialize();
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

        public void SetSplashVFXEnabled(bool _isEnable)
        {
            m_SyringeSplashVFX.SetVFXEnabled(_isEnable);
        }

        private void OnDisable()
        {
            m_ShrinkingPiece[0].OnChangeShrinkingValue -= CachedComponent.OnChangeShrinkingValue;
        }
    }
}