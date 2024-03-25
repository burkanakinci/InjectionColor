using UnityEngine;

namespace Game.Object
{
    public class Colored : CustomBehaviour
    {
        #region Fields

        [SerializeField] private ColoredVisual m_ColoredVisual;
        [SerializeField] private ColoredData m_ColoredData;
        [SerializeField] private Transform m_SyringeParent;
        [SerializeField] private Color m_ObjectColor;
        [SerializeField] private Vector3 m_SyringeParentShrinkEndPos;
        private Vector3 m_SyringeParentShrinkStartPos;
        public Color ObjectColor => m_ObjectColor;

        #endregion

        #region ExternalAccess

        public Transform SyringeTargetParent => m_SyringeParent;

        #endregion
        public override void Initialize()
        {
            m_ColoredVisual.Initialize(this);
            m_SyringeParentShrinkStartPos = m_SyringeParent.transform.localPosition;
        }

        public void DeinjectColor()
        {
            m_ColoredVisual.DeinjectColoredVisual();
        }

        public void DilationColored()
        {
            m_ColoredVisual.DilationColoredVisual();
        }
    }
}
