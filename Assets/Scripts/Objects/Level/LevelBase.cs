using Game.Manager;
using Game.StateMachine;
using Game.UI;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.Object
{
    public class LevelBase : MonoBehaviour, ILevel
    {
        [SerializeField] private CustomBehaviour[] m_ObjectsOnLevel;
        [SerializeField] private Color m_TargetColor;
        public virtual void OnSpawnLevel()
        {
            TargetColorMatchArea m_TargetColorMatchArea;
            PouringCupTarget m_PouringCupTarget;
            
            m_TargetColorMatchArea = GameManager.Instance.GetManager<UIManager>().GetPanel(UIPanelType.HudPanel)
                .GetArea<TargetColorMatchArea, HudAreaType>(HudAreaType.TargetMatchColorArea);
            m_TargetColorMatchArea.SetTargetColor(m_TargetColor);
            for (int i = 0; i < m_ObjectsOnLevel.Length; i++)
            {
                m_ObjectsOnLevel[i].Initialize();
            }
            m_PouringCupTarget = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup.PouringCupTarget;
            m_PouringCupTarget.SetPouringCupLiquidColor(m_TargetColor);
        }
    }
}