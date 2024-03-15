using System;
using System.Collections;
using Game.Object;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Game.Manager
{
    public class LevelManager : IManager
    {
        #region Fields
    
         private int m_CurrentLevelNumber;
         private int m_ActiveLevelDataNumber;
         private int m_MaxLevelDataCount;
         private JsonConverter m_JsonConverter;
         private AsyncOperationHandle<GameObject> m_LevelPrefabHandle;
         [SerializeField] private AddressableAssetGroup m_LevelAssetGroup;
    
        #endregion
    
        #region Actions
    
        public event Action OnCleanSceneObject;
    
        #endregion

        public void InitializeManager()
        {
            m_JsonConverter = GameManager.Instance.GetManager<JsonConverter>();
            m_MaxLevelDataCount = m_LevelAssetGroup.entries.Count;
        }

        private void LoadLevelPrefab()
        {
            Addressables.LoadAssetAsync<GameObject>(GetLevelPrefabPath()).Completed += OnLevelPrefabLoaded;
        }

        private void OnLevelPrefabLoaded(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                m_LevelPrefabHandle = obj;
                SpawnLevelPrefab(obj.Result.gameObject);
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
            if (m_LevelPrefabHandle.IsValid())
            {
                Addressables.ReleaseInstance(m_LevelPrefabHandle);
            }
        }

        private string GetLevelPrefabPath()
        {
            SetLevelNumber(m_JsonConverter.SavedPlayerData.PlayerLevel);
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

        [Button]
        public void SceneLoad()
        {
            SceneManager.LoadScene("Gameplay_scene 1");
        }
        
        public void CleanSceneObject()
        {
            OnCleanSceneObject?.Invoke();
        }
    }
}