using System;
using Game.Object;
using Game.StateMachine;
using Sirenix.OdinInspector;

namespace Game.GamePlayer
{
    public class Player : CustomBehaviour
    {
        [ShowInInspector]public PlayerStateMachine PlayerStateMachine { get; private set; }

        public override void Initialize()
        {
            PlayerStateMachine = new PlayerStateMachine(this);
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