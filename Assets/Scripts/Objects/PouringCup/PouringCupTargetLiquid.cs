using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupTargetLiquid : CustomBehaviour<PouringCupTarget>
    {
        [SerializeField] private MeshRenderer m_LiquidRenderer;

        public void SetLiquidColor(Color _color)
        {
            m_LiquidRenderer.material.color = _color;
        }
    }
}