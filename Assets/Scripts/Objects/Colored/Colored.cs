using UnityEngine;

namespace Game.Object
{
    public class Colored : CustomBehaviour
    {
        [SerializeField] private ColoredVisual m_ColoredVisual;
        [SerializeField] private ColoredData m_ColoredData;
        [SerializeField] private Transform m_SyringeParent;
       

        public override void Initialize()
        {
            m_ColoredVisual.Initialize(this);
        }

        public void DeinjectColor()
        {
            m_ColoredVisual.DeinjectColoredVisual();
        }
    }
}
