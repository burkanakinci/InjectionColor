using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Object;
using Game.StateMachine;
using Game.UI;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
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
        [SerializeField]private int m_MaxLevelDataCount;
        private JsonConverter m_JsonConverter;
        private AsyncOperationHandle<GameObject> m_LevelPrefabHandle;
        [SerializeField] private int m_StartRandomLevel;
        private FinishBGArea m_FinishBGArea;
        private PlayerStateMachine m_PlayerStateMachine;

        #endregion

        #region Actions

        public event Action OnCleanSceneObject;

        #endregion

        public void InitializeManager()
        {
            m_JsonConverter = GameManager.Instance.GetManager<JsonConverter>();
            m_FinishBGArea = GameManager.Instance.GetManager<UIManager>().GetPanel(UIPanelType.FinishPanel)
                .GetArea<FinishBGArea, FinishAreaType>(FinishAreaType.FinishBGArea);
            m_PlayerStateMachine = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine;
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

        private GameObject m_CurrentLevel;
        private void SpawnLevelPrefab(GameObject prefab)
        {
             m_CurrentLevel = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
             m_CurrentLevel.GetComponent<ILevel>()?.OnSpawnLevel();
             m_FinishBGArea.SetFinishBGEnabled(false).OnComplete(() =>
             {
                 m_PlayerStateMachine.ChangeStateTo(PlayerStates.RunState);
             });
        }
        [Button]
        private void UnloadLevelPrefab()
        {
            if (m_LevelPrefabHandle.IsValid())
            {
                Addressables.ReleaseInstance(m_LevelPrefabHandle);
            }

            if (m_CurrentLevel != null)
            {
                GameObject.Destroy(m_CurrentLevel);
            }
        }

        private string GetLevelPrefabPath()
        {
            SetLevelNumber(m_JsonConverter.SavedPlayerData.PlayerLevel);
            return "Assets/Prefabs/Levels/Level" + m_ActiveLevelDataNumber + ".prefab";
        }

        public void SetLevelNumber(int _levelNumber)
        {
            m_CurrentLevelNumber = _levelNumber;
            m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ?
                 (m_CurrentLevelNumber)
                : ((int)(UnityEngine.Random.Range(m_StartRandomLevel, (m_MaxLevelDataCount + 1))));
        }

        [Button]
        public void LoadLevel()
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