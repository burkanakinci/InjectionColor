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
            CreatePool();
        }
        
        private PooledObject m_TempSpawned;
        private void CreatePool()
        {
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

        private PoolPair m_TempSpawnedPoolPair;
        public T SpawnFromPool<T>(PooledObjectType _pooledObjectType) where T : PooledObject
        {
            m_TempSpawnedPoolPair = m_PooledLists.Find(x => x.PooledObjectType == _pooledObjectType);
            if( m_TempSpawnedPoolPair.PooledObjectsList == null)
            {
                return null;
            }

            if (m_TempSpawnedPoolPair.PooledObjectsList.Count > 0)
            {
                if (m_TempSpawnedPoolPair.PooledObjectsList[0] is T _objectType)
                {
                    m_TempSpawnedPoolPair.PooledObjectsList.Remove(_objectType);
                    _objectType.gameObject.SetActive(true);
                    _objectType.OnObjectSpawn();
                    return _objectType;
                }
            }
            else
            {
                var _spawnedPair = m_Pools.Find(x => x.PooledObjectType == _pooledObjectType);
                m_TempSpawned = GameObject.Instantiate(
                    m_Pools.Find(x => x.PooledObjectType == _pooledObjectType).PooledObjectPrefab
                    , GameManager.Instance.GetManager<Entities>().GetDeactiveParent(_spawnedPair.DeactiveParent)
                ).GetComponent<PooledObject>();
                m_TempSpawned.Initialize();
                m_TempSpawned.SetDeactiveParent(_spawnedPair.DeactiveParent);
                m_TempSpawned.SetPooledObjectType(_spawnedPair.PooledObjectType);
                m_TempSpawned.OnObjectSpawn();
                if (m_TempSpawned is T _spawnedType)
                {
                    return _spawnedType;
                }
            }
            return null;
        }

        private PoolPair m_TempAddedPoolPair;
        public void AddObjectPool(PooledObject _pooledObject)
        {
            m_TempAddedPoolPair = m_PooledLists.Find(x => x.PooledObjectType == _pooledObject.PooledObjectTag);
            if (m_TempAddedPoolPair.PooledObjectsList != null)
            {
                m_TempAddedPoolPair.PooledObjectsList.Add(_pooledObject);
            }
        }
    }
}