using DG.Tweening;
using Game.Manager;
using Game.UI;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCup : CustomBehaviour
    {
        #region Fields

        [SerializeField] private PouringCupVisual m_PouringCupVisual;
        [SerializeField] private PouringCupData m_PouringCupData;
        [SerializeField] private Transform m_SyringePouringParent;
        [SerializeField] private Transform m_SyringeStartParent;
        [SerializeField] private PouringCupTarget m_PouringCupTarget;

        #endregion

        #region External Access

        public Transform SyringePouringParent => m_SyringePouringParent;
        public Transform SyringeStartParent => m_SyringeStartParent;
        public PouringCupTarget PouringCupTarget => m_PouringCupTarget;

        #endregion

        private TargetColorMatchArea m_TargetMatchUIArea;

        public override void Initialize()
        {
            m_PouringCupVisual.Initialize(this);
            m_TargetMatchUIArea = GameManager.Instance.GetManager<UIManager>().GetPanel(UIPanelType.HudPanel)
                .GetArea<TargetColorMatchArea, HudAreaType>(HudAreaType.TargetMatchColorArea);
        }

        public void SetColorOnDeinject(Color _addedColor)
        {
            m_PouringCupVisual.PouringCupLiquid.SetTargetColorOnDeinject(_addedColor);
            m_PouringCupVisual.PouringCupLiquid.StartColorChangeTween(m_PouringCupData.ChangePouringLiquidColorPair);
        }
    }
}