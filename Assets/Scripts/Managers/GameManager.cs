using UnityEngine;
using System;
using DG.Tweening;
using Game.Utilities.Constants;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour,IManager
    {
        public static GameManager Instance { get; private set; }
        
        #region Fields
    
        [SerializeReference] [SerializeField] 
        private IManager[] m_ManagerFields;
        
        #endregion

        public void Awake()
        {
            Instance = this;
    
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
    
            InitializeManager();
        }
        public void InitializeManager()
        {
            DOTween.SetTweensCapacity(2500,250);
            for (int i = 0; i < m_ManagerFields.Length; i++)
            {
                m_ManagerFields[i].InitializeManager();
            }
        }

        public T GetManager<T>() where T : IManager
        {
            ManagerType managerType = GetManagerType<T>();

            return (T)m_ManagerFields[(int)managerType];
        }

        private ManagerType GetManagerType<T>() where T : IManager
        {
            if (typeof(T) == typeof(JsonConverter))
            {
                return ManagerType.JSONConverter;
            }
            else if (typeof(T) == typeof(PlayerManager))
            {
                return ManagerType.PlayerManager;
            }

            throw new ArgumentException($"Manager type not supported: {typeof(T)}");
        }
    }
}