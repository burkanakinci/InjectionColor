using System;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.Object
{
    public class TutorialLevel : LevelBase
    {
        [SerializeField] private TutorialArrow m_TutorialArrow;
        private RunState m_RunState;
        public override void OnSpawnLevel()
        {
            base.OnSpawnLevel();
            m_TutorialArrow.Initialize(this);
            m_RunState = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine
                .GetPlayerState(PlayerStates.RunState) as RunState;
            m_RunState.OnRayCollidedEvent += OnRayCollidedEvent;
        }

        private void OnRayCollidedEvent(GameObject _collided)
        {
            m_TutorialArrow.DisableArrow();
            m_RunState.OnRayCollidedEvent -= OnRayCollidedEvent;
        }
    }
}
