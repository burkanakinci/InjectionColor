using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    public class PouringCupTarget : CustomBehaviour<PouringCup>
    {
        [SerializeField] private PouringCupTargetLiquid m_TargetLiquid;
        private IPlayerState m_IdleState;
        
        public override void Initialize(PouringCup _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_IdleState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine.GetPlayerState(PlayerStates.IdleState);
            m_IdleState.OnEnterEvent += OnStart;
        }
        
        private void OnStart()
        {
            SetPouringCupLiquidColor(Color.white);
        }

        public void SetPouringCupLiquidColor(Color _color)
        {
            m_TargetColor = _color;
            m_TargetLiquid.SetLiquidColor(_color);
        }

        private Color m_TargetColor;
        private const float m_PercentMultiply = 100.0f / 3.0f;
        private float m_ContainsValue;
        private float m_ColorTotalDist;
        [Button]
        public float GetContainsPlayerColor(Color _color)
        {
            m_ColorTotalDist = 0.0f;
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.r - _color.r);
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.g - _color.g);
            m_ColorTotalDist += Mathf.Abs(m_TargetColor.b - _color.b);
            m_ColorTotalDist = 3.0f - m_ColorTotalDist;
            m_ContainsValue = m_ColorTotalDist * m_PercentMultiply;

            return m_ContainsValue;
        }
        private void OnDisable()
        {
            if(m_IdleState != null)
                m_IdleState.OnEnterEvent -= OnStart;
        }
    }
}