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
                m_ShrinkingPiece[i].ShrinkObject();
            }
        }

        public void ChangeColorless()
        {
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].ChangeColorless();
            }
        }

        public void DilationColoredVisual()
        {
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].DilationDelayCallTween();
            }
        }
    }
}