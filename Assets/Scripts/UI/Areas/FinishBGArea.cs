using DG.Tweening;
using Game.Manager;
using Game.StateMachine;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.UI
{
    public class FinishBGArea : UIArea
    {
        [SerializeField] private RectTransform m_BGTransform;
        [SerializeField] private FinishBGMask m_FinishBGMask;
        private PlayerStateMachine m_PlayerStateMachine;

        #region External Access

        public FinishBGMask FinishBgMask => m_FinishBGMask;

        #endregion
        public override void Initialize(UIPanel _cachedComponent)
        {
            base.Initialize(_cachedComponent);
            m_PlayerStateMachine = GameManager.Instance.GetManager<PlayerManager>().Player.PlayerStateMachine;
            m_FinishBGMask.Initialize();
            m_BGTransform.sizeDelta = new Vector2(
                Screen.width,
                Screen.height
                );
        }

        public override void ShowArea()
        {
            base.ShowArea();
            SetFinishBGEnabled(true).OnComplete(() =>
            {
                m_PlayerStateMachine.ChangeStateTo(PlayerStates.IdleState);
            });
        }

        public Tween SetFinishBGEnabled(bool _isEnable)
        {
            if (_isEnable)
            {
                return m_FinishBGMask.ShowBG();
            }
            else
            {
                return m_FinishBGMask.HideBG();
            }
        }
    }
}
