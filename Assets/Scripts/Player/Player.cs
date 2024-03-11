using System;

public class Player : CustomBehaviour<PlayerManager>
{
    private PlayerData m_PlayerData;

    public PlayerStateMachine PlayerStateMachine { get; private set; }
    #region Event
    public event Action OnChangedCoinValue;
    #endregion

    #region ExternalAccess
    public int PlayerLevel => m_PlayerData.PlayerLevel;
    public int PlayerCoin => m_PlayerData.PlayerCoin;
    #endregion
    public override void Initialize(PlayerManager _playerManager)
    {
        GameManager.Instance.JsonConverter.LoadPlayerData(ref m_PlayerData);
        PlayerStateMachine = new PlayerStateMachine(this);
    }
    public void SetPlayerCoin(int _coin)
    {
        m_PlayerData.PlayerCoin = _coin;
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
        OnChangedCoinValue?.Invoke();
    }
    public void SetPlayerLevel(int _level)
    {
        m_PlayerData.PlayerLevel = _level;
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
    }

    #region Updates

    private void Update()
    {
        PlayerStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        PlayerStateMachine.PhysicalUpdate();
    }

    #endregion
}
