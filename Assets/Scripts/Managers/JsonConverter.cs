using UnityEngine;

namespace Game.Manager
{
    public class JsonConverter : IManager
    {
        private string m_TempJsonData;
        private PlayerData m_PlayerData;
        public void InitializeManager()
        {
            LoadPlayerData();
            Debug.Log(m_PlayerData.PlayerCoin);
        }

        public void LoadPlayerData()
        {
            var data = PlayerPrefs.GetString(Constant.PLAYER_DATA);

            if (string.IsNullOrEmpty(data))
            {

                m_PlayerData = new PlayerData
                {
                    PlayerLevel = 1,
                    PlayerCoin = 100,
                };

                SavePlayerData(m_PlayerData);
            }
            else
            {
                m_PlayerData = JsonUtility.FromJson<PlayerData>(data);
            }
        }
        public void SavePlayerData(PlayerData _playerData)
        {
            m_TempJsonData = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString((Constant.PLAYER_DATA), (m_TempJsonData));
            PlayerPrefs.Save();
        }
    }
}
