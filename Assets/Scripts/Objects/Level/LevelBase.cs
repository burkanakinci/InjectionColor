using UnityEngine;

namespace Game.Object
{
    public class LevelBase : MonoBehaviour, ILevel
    {
        [SerializeField] private CustomBehaviour[] m_ObjectsOnLevel;

        public void OnSpawnLevel()
        {
            for (int i = 0; i < m_ObjectsOnLevel.Length; i++)
            {
                m_ObjectsOnLevel[i].Initialize();
            }
        }
    }
}