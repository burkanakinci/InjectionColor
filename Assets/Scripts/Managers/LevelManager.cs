using System;
using System.Collections;
using Game.Object;
using Sirenix.OdinInspector;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Manager
{
    public class LevelManager : IManager
    {
        #region Fields
    
         private int m_CurrentLevelNumber;
         private int m_ActiveLevelDataNumber;
         private int m_MaxLevelDataCount;
    
        #endregion
    
        #region Actions
    
        public event Action OnCleanSceneObject;
    
        #endregion

        #region AddressableLevelSpawn
        

        private AsyncOperationHandle<GameObject> levelPrefabHandle;
        [SerializeField] private AddressableAssetGroup m_LevelAssetGroup;
        
        public void InitializeManager()
        {
            m_MaxLevelDataCount = m_LevelAssetGroup.entries.Count;
            // m_MaxLevelDataCount = Resources.LoadAll("LevelDatas", typeof(LevelData)).Length;
            // HudPanel _hudPanel = GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel) as HudPanel;
            // m_JellyOnUIArea =_hudPanel.GetArea<HudAreaType>(HudAreaType.JellyOnUIArea) as JellyOnUIArea;
            // m_TargetJellyArea = _hudPanel.GetArea<HudAreaType>(HudAreaType.TargetJellyArea) as TargetJellyArea;
            // m_JellyGrid = GameManager.Instance.Entities.GetSceneObject(SceneObjectType.JellyGrid) as JellyGrid;
        }

        private void LoadLevelPrefab()
        {
            Addressables.LoadAssetAsync<GameObject>(GetLevelPrefabPath()).Completed += OnLevelPrefabLoaded;
        }

        private void OnLevelPrefabLoaded(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                levelPrefabHandle = obj;
                SpawnLevelPrefab(obj.Result.gameObject);
            }
            else
            {
                Debug.LogError("Failed to load level prefab: " + obj.OperationException);
            }
        }

        private void SpawnLevelPrefab(GameObject prefab)
        {
            GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            prefab.GetComponent<LevelBase>().Initialize();
            prefab.GetComponent<LevelBase>().OnSpawnLevel();
        }

        private void UnloadLevelPrefab()
        {
            if (levelPrefabHandle.IsValid())
            {
                Addressables.ReleaseInstance(levelPrefabHandle);
            }
        }

        private string GetLevelPrefabPath()
        {
            SetLevelNumber(GameManager.Instance.GetManager<JsonConverter>().GetSavedPlayerData().PlayerLevel);
            return "Assets/Prefabs/Levels/Level"+m_ActiveLevelDataNumber+".prefab";
        }
        
        public void SetLevelNumber(int _levelNumber)
        {
            
            m_CurrentLevelNumber = _levelNumber;
            m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount)
                ? (m_CurrentLevelNumber)
                : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        }
        
        [Button]
        public void Asd()
        {
            UnloadLevelPrefab();
            LoadLevelPrefab();
        }

        #endregion

        public void CreateLevel()
        {
            CleanSceneObject();
            GetLevelData();
            StartSpawnSceneObjects();
        }
    
        public void CleanSceneObject()
        {
            OnCleanSceneObject?.Invoke();
        }
    
        public void GetLevelData()
        {
            //CurrentLevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "LevelData");
        }
        private void StartSpawnSceneObjects()
        {
        }
        
    }
}