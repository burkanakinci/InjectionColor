using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Manager;
using Game.Object;
using Game.Utilities.Constants;

namespace Game.Manager
{
    public class Entities : IManager
    {
        [Serializable]
        public struct SceneObjectPair
        {
            public SceneObjectType SceneObjectType;
            public CustomBehaviour SceneObject;
        }

        #region Parent Objects

        [Header("Parent Objects")] 
        [SerializeField] private Transform[] m_DeactiveParents;
        [SerializeField] private Transform[] m_ActiveParents;

        #endregion


        #region SceneObjects

        [Space(10)] [Header("Scene Objects")] 
        [SerializeField] private List<SceneObjectPair> m_SceneObjects;

        #endregion

        public void InitializeManager()
        {
            m_SceneObjects.ForEach(_object => { _object.SceneObject.Initialize(); });
        }

        #region Getter

        public Transform GetActiveParent(ActiveParents _activeParent)
        {
            return m_ActiveParents[(int)_activeParent];
        }

        public Transform GetDeactiveParent(DeactiveParents _deactiveParent)
        {
            return m_DeactiveParents[(int)_deactiveParent];
        }

        public CustomBehaviour GetSceneObject(SceneObjectType _objectType)
        {
            return m_SceneObjects.FirstOrDefault(x => x.SceneObjectType == _objectType).SceneObject;
        }

        #endregion
    }
}