using UnityEngine;

namespace Game.Object
{
    public class PouringCup : CustomBehaviour
    {
        [SerializeField] private PouringCupVisual m_PouringCupVisual;
        [SerializeField] private Transform m_SyringePouringParent;
        public Transform SyringePouringParent => m_SyringePouringParent;
        public override void Initialize()
        {
            m_PouringCupVisual.Initialize(this);
        }
    }
}
