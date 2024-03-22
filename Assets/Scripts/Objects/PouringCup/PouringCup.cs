using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCup : CustomBehaviour
    {
        [SerializeField] private PouringCupVisual m_PouringCupVisual;
        [SerializeField] private PouringCupData m_PouringCupData;
        [SerializeField] private Transform m_SyringePouringParent;
        public Transform SyringePouringParent => m_SyringePouringParent;
       
        public override void Initialize()
        {
            m_PouringCupVisual.Initialize(this);
        }

        public void SetColorOnDeinject(Color _addedColor)
        {
            m_PouringCupVisual.PouringCupLiquid.SetTargetColorOnDeinject(_addedColor);
            m_PouringCupVisual.PouringCupLiquid.StartColorChangeTween(m_PouringCupData.ChangePouringLiquidColorPair);
        }

        public void SetPouringLiquidNormalSpeedUp()
        {
            m_PouringCupVisual.PouringCupLiquid.SetLiquidNormalSpeed(m_PouringCupData.UpNormalSpeedPourLiquidPair)
                .OnComplete(() =>
                {
                    m_PouringCupVisual.PouringCupLiquid.SetLiquidNormalSpeed(m_PouringCupData
                        .DownNormalSpeedPourLiquidPair);
                });
        }
    }
}
