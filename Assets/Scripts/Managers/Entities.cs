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
        public int Asd;
        #region Parent Objects

        [Header("Parent Objects")] 
        [SerializeField] private Transform[] m_DeactiveParents;
        [SerializeField] private Transform[] m_ActiveParents;

        #endregion
        
        #region SceneObjects

        [Space(10)] [Header("Scene Objects")] 
        [SerializeField] private SceneObjectPair[] m_SceneObjects;

        #endregion

        #region Colored Objects Materials

        [Header("Colored Objects Materials")] 
        [SerializeField] private ColoredObjectsMaterial[] m_ColoredObjectsMaterials;

        #endregion

        public void InitializeManager()
        {
            for (int i = 0; i < m_SceneObjects.Length; i++)
            {
                m_SceneObjects[i].SceneObject.Initialize();
            }
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
            return m_SceneObjects[(int)_objectType].SceneObject;
        }

        public Material GetColoredObjectsMaterial(ColoredObjectMaterialType _coloredType)
        {
            return m_ColoredObjectsMaterials[(int)_coloredType].ColoredObjectMaterial;
        }

        #endregion
    }
    [Serializable]
    public struct SceneObjectPair
    {
        #region Datas

        [SerializeField] private CustomBehaviour m_SceneObject;

        #endregion

        #region ExternalAccess

        public CustomBehaviour SceneObject => m_SceneObject;

        #endregion

    }

    [Serializable]
    public struct ColoredObjectsMaterial
    {
        #region Datas

        [SerializeField] private Material m_ColoredObjectMaterial;

        #endregion

        #region ExternalAccess

        public Material ColoredObjectMaterial => m_ColoredObjectMaterial;

        #endregion
    }
}