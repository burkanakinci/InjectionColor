using System;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupTargetLiquid : CustomBehaviour<PouringCupTarget>
    {
        [SerializeField] private MeshRenderer m_LiquidRenderer;
        private IPlayerState m_IdleState;

        public override void Initialize(PouringCupTarget _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_IdleState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine.GetPlayerState(PlayerStates.IdleState);
            m_IdleState.OnEnterEvent += OnStart;
        }

        private void OnStart()
        {
            SetLiquidColor(Color.white);
        }

        public void SetLiquidColor(Color _color)
        {
            m_LiquidRenderer.material.color = _color;
        }

        private void OnDisable()
        {
            if(m_IdleState != null)
                m_IdleState.OnEnterEvent -= OnStart;
        }
    }
}