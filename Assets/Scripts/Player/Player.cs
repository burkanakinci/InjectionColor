using Game.Object;
using Game.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GamePlayer
{
    public class Player : CustomBehaviour
    {
        #region Player Fields

        [ShowInInspector]public PlayerStateMachine PlayerStateMachine { get; private set; }

        #endregion

        #region Player Objects

        [SerializeField] private Syringe m_Syringe;
        public Syringe PlayerSyringe => m_Syringe;

        #endregion

        public override void Initialize()
        {
            PlayerStateMachine = new PlayerStateMachine(this);
            m_Syringe.Initialize();
        }

        private void FixedUpdate()
        {
            PlayerStateMachine.PhysicalUpdate();
        }

        private void Update()
        {
            PlayerStateMachine.LogicalUpdate();
        }
    }
}