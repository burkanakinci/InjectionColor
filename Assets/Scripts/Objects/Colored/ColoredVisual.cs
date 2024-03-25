using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class ColoredVisual : CustomBehaviour<Colored>
    {
        [SerializeField] private ShrinkingPiece[] m_ShrinkingPiece;

        public override void Initialize(Colored _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].Initialize(this);
            }
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
    }
}