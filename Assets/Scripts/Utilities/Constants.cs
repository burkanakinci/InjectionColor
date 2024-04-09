namespace Game.Utilities.Constants
{
    public struct PlayerData
    {
        public int PlayerLevel;
        public int PlayerCoin;
    }

    public struct Constant
    {
        public const string PLAYER_DATA = "PlayerSavedData";
    }

    public struct SyringeLiquidMaterial
    {
        public const string FULNESS = "_FillAmount"; 
        public const string TOP_COLOR = "_TopColor"; 
        public const string SIDE_COLOR = "_SideColor"; 
    }

    public enum PooledObjectType
    {
    }

    public struct ObjectTags
    {
    }

    public enum PlayerStates
    {
        IdleState = 0,
        RunState = 1,
        WinState = 2,
        FailState = 3,
        InjectColorState = 4,
        GeneralState = 5,
    }

    public enum ObjectsLayer
    {
        Default = 0,
        Colored = 6,
    }

    public enum UIPanelType
    {
        IdlePanel = 0,
        HudPanel = 1,
        FinishPanel = 2,
        CommonPanel = 3,
    }

    public enum FinishAreaType
    {
        FinishBGArea = 0,
    }

    public enum HudAreaType
    {
        TargetMatchColorArea = 0,
    }

    public enum ActiveParents
    {
    }

    public enum DeactiveParents
    {
    }

    public enum ListOperations
    {
        Adding = 0,
        Removing = 1,
        Clear = 2,
    }

    public enum SceneObjectType
    {

    }
    public enum ColoredObjectMaterialType
    {
        Colorless = 0,
        LightColorless = 1,
    }

    public enum ManagerType
    {
        JSONConverter = 0,
        PlayerManager = 1,
        InputManager = 2,
        UIManager = 3,
        ObjectPool = 4,
        Entities = 5,
        LevelManager = 6,
    }

}