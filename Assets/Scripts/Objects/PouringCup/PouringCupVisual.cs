using UnityEngine;

namespace Game.Object
{
    public class PouringCupVisual : CustomBehaviour<PouringCup>
    {
        [SerializeField] private PouringCupLiquid m_PouringCupLiquid;
        public override void Initialize(PouringCup _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_PouringCupLiquid.Initialize(this);
        }
    }
}
