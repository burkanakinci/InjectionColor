using UnityEngine;

namespace Game.Manager
{
    public class CameraManager : IManager
    {
        [SerializeField] private Camera m_CurrentCamera;

        public Camera CurrentCamera => m_CurrentCamera;

        public void InitializeManager()
        {
        }
    }
}