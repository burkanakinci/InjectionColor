using Game.Manager;
using UnityEngine;

namespace Game.Object
{
    public class LevelBase : MonoBehaviour, ILevel
    {
        [SerializeField] private CustomBehaviour[] m_ObjectsOnLevel;
        [SerializeField] private Color m_TargetColor;
        private PouringCupTarget m_PouringCupTarget;
        public void OnSpawnLevel()
        {
            for (int i = 0; i < m_ObjectsOnLevel.Length; i++)
            {
                m_ObjectsOnLevel[i].Initialize();
            }
            m_PouringCupTarget = GameManager.Instance.GetManager<PlayerManager>().Player.PouringCup.PouringCupTarget;
            m_PouringCupTarget.SetPouringCupLiquidColor(m_TargetColor);
        }
    }
}