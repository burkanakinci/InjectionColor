using UnityEngine;

namespace Game.Object
{
    public class PouringCupTarget : CustomBehaviour<PouringCup>
    {
        [SerializeField] private PouringCupTargetLiquid m_TargetLiquid;
        private Color m_CurrentTargetColor;
        public void SetPouringCupLiquidColor(Color _color)
        {
            m_CurrentTargetColor = _color;
            m_TargetLiquid.SetLiquidColor(_color);
        }
    }
}