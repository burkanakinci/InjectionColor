using UnityEngine;
using System;
using Game.GamePlayer;
using Game.Manager;
using Game.Object;
using Game.Utilities.Constants;

namespace Game.StateMachine
{
    public class RunState : IPlayerState
    {
        public Action OnEnterEvent { get; set; }
        public Action OnExitEvent { get; set; }
        private Player m_Player;
        private InputManager m_InputManager;
        private Camera m_CurrentCamera;

        public RunState(Player _player)
        {
            m_Player = _player;
            m_MergingLayerMask = 1 << (int)ObjectsLayer.Colored;
            m_InputManager = GameManager.Instance.GetManager<InputManager>();
            m_CurrentCamera = GameManager.Instance.GetManager<CameraManager>().CurrentCamera;
        }

        public void Enter()
        {
            // GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel).ShowPanel();
            ResetRayCollidedEvent();
            // OnEnterEvent?.Invoke();
        }

        public void SubscribeInputTouchDown()
        {
            m_InputManager.OnTouchDown -= ClickedRay;
            m_InputManager.OnTouchDown += ClickedRay;
        }

        public void UnsubscribeInputTouchDown()
        {
            m_InputManager.OnTouchDown -= ClickedRay;
        }
        public void ResetRayCollidedEvent()
        {
            m_RayCollidedEvent = ClickedColoredObject;
            SubscribeInputTouchDown();
        }

        private Action<GameObject> m_RayCollidedEvent;
        private RaycastHit m_MergingHit;
        private Ray m_MergingRay;
        private int m_MergingLayerMask;
        private Colored m_ClickedColored;

        public void ClickedRay()
        {
            m_MergingRay = m_CurrentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_MergingRay, out m_MergingHit, 1000f, m_MergingLayerMask))
            {
                m_RayCollidedEvent?.Invoke(m_MergingHit.collider.gameObject);
            }
        }

        private void ClickedColoredObject(GameObject _clickedObject)
        {
            if (_clickedObject.TryGetComponent(out Colored _colored))
            {
                m_Player.PlayerSyringe.StartInjectColored(_colored);
                m_Player.PlayerStateMachine.ChangeStateTo(PlayerStates.InjectColorState);
            }
        }

        public void UpdateLogic()
        {
            m_InputManager.UpdateInput();
        }

        public void UpdatePhysic()
        {
        }

        public void Exit()
        {
            // UnsubscribeInputTouchDown();
            // m_RayCollidedEvent = null;
            // OnExitEvent?.Invoke();
        }
    }
}