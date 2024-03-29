using System;
using System.Collections.Generic;
using Game.GamePlayer;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.StateMachine
{
    public class PlayerStateMachine
    {
        #region Fields

        [SerializeField]private IPlayerState m_CurrentState;
        [SerializeField]private IPlayerState m_GeneralState;
        [SerializeField]private IPlayerState[] m_States;

        #endregion

        public event Action<PlayerStates> OnPlayerStateChanged;

        public PlayerStateMachine(Player _player)
        {
            m_States = new IPlayerState[6];
            m_States[0] = new IdleState(_player);
            m_States[1] = new RunState(_player);
            m_States[2] = new WinState(_player);
            m_States[3] = new FailState(_player);
            m_States[4] = new InjectColorState(_player);
            m_States[5] = new GeneralState(_player);

            m_CurrentState = GetPlayerState(PlayerStates.IdleState);
            m_GeneralState = GetPlayerState(PlayerStates.GeneralState);
        }

        public bool CompareState(PlayerStates _state)
        {
            return (m_CurrentState == m_States[(int)_state]);
        }

        public IPlayerState GetPlayerState(PlayerStates _state)
        {
            return m_States[(int)_state];
        }

        public void ChangeStateTo(PlayerStates state, bool _force = false)
        {
            if (m_CurrentState != m_States[(int)state] || _force)
            {
                Exit();
                m_CurrentState = m_States[(int)state];
                Enter();
                OnPlayerStateChanged?.Invoke(state);
            }
        }

        public void Enter()
        {
            m_CurrentState.Enter();
            m_GeneralState.Enter();
        }

        public void LogicalUpdate()
        {
            m_CurrentState.UpdateLogic();
            m_GeneralState.UpdateLogic();
        }

        public void PhysicalUpdate()
        {
            m_CurrentState.UpdatePhysic();
            m_GeneralState.UpdatePhysic();
        }

        public void Exit()
        {
            m_CurrentState.Exit();
            m_GeneralState.Exit();
        }
    }
}