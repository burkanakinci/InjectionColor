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
    public const string GRID_NODE = "GridNode";
    public const string COLORFUL_GRID_NODE = "ColorfulGridNode";
    public const string JELLY = "Jelly";
    public const string  JELLY_ON_UI="JellyOnUI";
    public const string JELLY_IMAGE_ON_UI = "JellyImageOnUI";
    public const string JELLY_SCATTER = "JellyScatter";
    public const string TARGET_JELLY_ROW = "TargetJellyRow";
    public const string JELLY_SCATTER_PARTICLE = "JellyScatterParticle";
    public const string FRACTURE_NODE = "FractureNode";
}
public struct ObjectTags
{
    public const string GRID_NODE = "GridNode";
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
    GridNode = 6,
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
    JellyOnUIArea = 0,
    TargetJellyArea = 1,
}
public enum ActiveParents
{
    GridNodeParent = 0,
    ColorfulGridNodeParent = 1,
    JellyParents = 2,
    JellyScatter = 3,
    JellyScatterParticle = 4,
    FractureNode = 5,
}
public enum DeactiveParents
{
    GridNodeParent = 0,
    ColorfulGridNodeParent = 1,
    JellyParents = 2,
    JellyOnUI = 3,
    JellyImageOnUI = 4,
    JellyScatter = 5,
    TargetJellyRow=6,
    JellyScatterParticle = 7,
    FractureNode =8,
}

public enum SceneObjectType
{
    JellyGrid = 0,
}
public enum ListOperations
{
    Adding,
    Removing,
}
