using System;
using System.Collections.Generic;
using Game.Manager;
using Game.Object;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.PoolSystem
{
    public sealed class ObjectPool : IManager
    {
        [Serializable]
        public struct PooledObjectPair
        {
            #region Datas

            [SerializeField] private PooledObjectType m_PooledObjectType;
            [SerializeField] private PooledObject m_PooledObjectPrefab;
            [SerializeField] private int m_SpawnedSize;
            [SerializeField] private DeactiveParents m_DeactiveParent;

            #endregion

            #region ExternalAccess

            public PooledObjectType PooledObjectType => m_PooledObjectType;
            public PooledObject PooledObjectPrefab => m_PooledObjectPrefab;
            public int SpawnedSize => m_SpawnedSize;
            public DeactiveParents DeactiveParent => m_DeactiveParent;

            #endregion
        }
        [Serializable]
        public struct PoolPair
        {
            public PooledObjectType PooledObjectType;
            public List<PooledObject> PooledObjectsList;
        }

        [SerializeField] private List<PooledObjectPair> m_Pools;
        private List<PoolPair> m_PooledLists;

        public void InitializeManager()
        {
            PooledObject m_TempSpawned;
            m_PooledLists = new List<PoolPair>();
            
            for (int i = 0; i < m_Pools.Count; i++)
            {
                m_PooledLists.Add(
                    new PoolPair()
                    {
                        PooledObjectType =  m_Pools[i].PooledObjectType,
                        PooledObjectsList = new List<PooledObject>(),
                    }
                    );
                
                for (int j = 0; j < m_Pools[i].SpawnedSize; j++)
                {
                    m_TempSpawned = GameObject.Instantiate(m_Pools[i].PooledObjectPrefab
                        , GameManager.Instance.GetManager<Entities>().GetDeactiveParent(m_Pools[i].DeactiveParent)
                        ).GetComponent<PooledObject>();
                     m_TempSpawned.gameObject.SetActive(false);
                     m_TempSpawned.Initialize();
                     m_TempSpawned.SetDeactiveParent(m_Pools[i].DeactiveParent);
                     m_TempSpawned.SetPooledObjectType(m_Pools[i].PooledObjectType);
                     m_PooledLists[i].PooledObjectsList.Add(m_TempSpawned);
                }
            }
        }
    }
}