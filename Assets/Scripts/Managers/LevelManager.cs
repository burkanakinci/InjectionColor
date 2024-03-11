using System;

public class LevelManager : CustomBehaviour
{
    #region Fields
    

    #endregion

    #region Actions

    public event Action OnCleanSceneObject;

    #endregion

    public override void Initialize()
    {
    }

    public void SetLevelNumber(int _levelNumber)
    {
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
    }
    private void StartSpawnSceneObjects()
    {

    }
}