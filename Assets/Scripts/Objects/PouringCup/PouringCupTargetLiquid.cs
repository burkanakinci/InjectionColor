using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupTargetLiquid : CustomBehaviour<PouringCupTarget>
    {
        [SerializeField] private MeshRenderer m_LiquidRenderer;
        private Color m_TargetColor;
        private const float m_PercentMultiply = 100.0f / 3.0f;
        private float m_ContainsValue;

        public void SetLiquidColor(Color _color)
        {
            m_TargetColor = _color;
            m_LiquidRenderer.material.color = m_TargetColor;
        }

        private float m_ColorTotalDist;
        [Button]
        public void ContainsPlayerColor(Color _color)
        {
            m_ColorTotalDist = 0.0f;
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.r - _color.r);
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.g - _color.g);
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.b - _color.b);
            m_ColorTotalDist = 3.0f - m_ColorTotalDist;
            m_ContainsValue = m_ColorTotalDist * m_PercentMultiply;
        }
    }
}