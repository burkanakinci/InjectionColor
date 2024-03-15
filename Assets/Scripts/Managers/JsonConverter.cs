using UnityEngine;
using Game.Utilities.Constants;

namespace Game.Manager
{
    public class JsonConverter : IManager
    {
        public PlayerData SavedPlayerData { get; private set; }
        public void InitializeManager()
        {
            LoadPlayerData();
        }

        public void LoadPlayerData()
        {
            var data = PlayerPrefs.GetString(Constant.PLAYER_DATA);

            if (string.IsNullOrEmpty(data))
            {

                var _playerData = new PlayerData
                {
                    PlayerLevel = 1,
                    PlayerCoin = 100,
                };

                SavePlayerData(_playerData);
            }
            SavedPlayerData = GetSavedPlayerData();
        }

        private PlayerData GetSavedPlayerData()
        {
            var data = PlayerPrefs.GetString(Constant.PLAYER_DATA);
            return JsonUtility.FromJson<PlayerData>(data);
        }
        public void SavePlayerData(PlayerData _playerData)
        {
            string _tempJsonData;
            _tempJsonData = JsonUtility.ToJson(_playerData);
            PlayerPrefs.SetString((Constant.PLAYER_DATA), (_tempJsonData));
            PlayerPrefs.Save();
            SavedPlayerData = _playerData;
        }
    }
}
