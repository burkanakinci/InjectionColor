using UnityEngine;

namespace Game.Object
{
    public class Syringe : CustomBehaviour
    {
        [SerializeField] private SyringeVisual m_SyringeVisual;
        [SerializeField] private SyringeData m_SyringeData;
        public override void Initialize()
        {
            m_SyringeVisual.Initialize(this);
        }

        public void SyringeColored(Colored _colored)
        {
            _colored.DeinjectColor();
        }
    }
}