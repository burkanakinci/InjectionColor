using System;

namespace Game.Manager
{
    public class LevelManager : IManager
    {
        #region Fields
    
        // public LevelData CurrentLevelData { get; private set; }
        // private int m_CurrentLevelNumber;
        // private int m_ActiveLevelDataNumber;
        // private int m_MaxLevelDataCount;
    
        #endregion
    
        #region Actions
    
        public event Action OnCleanSceneObject;
    
        #endregion
    
        public void InitializeManager()
        {
            // m_MaxLevelDataCount = Resources.LoadAll("LevelDatas", typeof(LevelData)).Length;
            // HudPanel _hudPanel = GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel) as HudPanel;
            // m_JellyOnUIArea =_hudPanel.GetArea<HudAreaType>(HudAreaType.JellyOnUIArea) as JellyOnUIArea;
            // m_TargetJellyArea = _hudPanel.GetArea<HudAreaType>(HudAreaType.TargetJellyArea) as TargetJellyArea;
            // m_JellyGrid = GameManager.Instance.Entities.GetSceneObject(SceneObjectType.JellyGrid) as JellyGrid;
        }
    
        public void SetLevelNumber(int _levelNumber)
        {
            // m_CurrentLevelNumber = _levelNumber;
            // m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount)
            //     ? (m_CurrentLevelNumber)
            //     : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        }
    
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