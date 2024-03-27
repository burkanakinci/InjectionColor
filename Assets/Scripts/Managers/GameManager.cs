using UnityEngine;
using System;
using DG.Tweening;
using Game.Utilities.Constants;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour, IManager
    {
        public static GameManager Instance { get; private set; }

        #region Fields

        [SerializeReference] [SerializeField] private IManager[] m_ManagerFields;

        #endregion

        public void Awake()
        {
            Instance = this;

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            InitializeManager();
        }

        private void Start()
        {
            GetManager<LevelManager>().LoadLevel();
        }

        public void InitializeManager()
        {
            DOTween.SetTweensCapacity(2500, 250);
            for (int i = 0; i < m_ManagerFields.Length; i++)
            {
                m_ManagerFields[i].InitializeManager();
            }
        }

        public T GetManager<T>() where T : class, IManager
        {
            foreach (IManager _manager in m_ManagerFields)
            {
                if (_manager is T typedManager)
                {
                    return typedManager;
                }
            }
            
            return null;
        }
    }
}