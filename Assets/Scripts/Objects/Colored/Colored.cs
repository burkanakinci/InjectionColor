using System.Linq;
using UnityEngine;

namespace Game.Object
{
    public class Colored : CustomBehaviour
    {
        [SerializeField] private ColoredVisual m_ColoredVisual;
        [SerializeField] private Transform m_SyringeParent;
        [SerializeField] private ShrinkingPiece[] m_ShrinkingPiece;

        public override void Initialize()
        {
            m_ColoredVisual.Initialize(this);
            for (int i = 0; i < m_ShrinkingPiece.Length; i++)
            {
                m_ShrinkingPiece[i].Initialize(this);
            }
        }
    }
}
