using Game.Object;
using Game.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GamePlayer
{
    public class Player : CustomBehaviour
    {
        #region Player Fields

        [ShowInInspector]
        public PlayerStateMachine PlayerStateMachine { get; private set; }

        #endregion

        #region Player Objects

        [SerializeField] private Syringe m_Syringe;
        [SerializeField] private PouringCup m_PouringCup;
        public Syringe PlayerSyringe => m_Syringe;
        public PouringCup PouringCup => m_PouringCup;

        #endregion

        public override void Initialize()
        {
            PlayerStateMachine = new PlayerStateMachine(this);
            m_Syringe.Initialize();
            m_PouringCup.Initialize();
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