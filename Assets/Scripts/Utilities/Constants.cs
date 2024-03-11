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

    public struct PooledObjectTags
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
        GeneralState = 4,
    }

    public enum ObjectsLayer
    {
        Default = 0,
    }

    public enum UIPanelType
    {
        IdlePanel = 0,
        RunPanel = 1,
        FinishPanel = 2,
    }

    public enum FinishAreaType
    {
        WinArea = 0,
        FailArea = 1,
    }

    public enum HudAreaType
    {
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

    public enum ManagerType
    {
        JSONConverter = 0,
        PlayerManager = 1,
    }

}