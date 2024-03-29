using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class NextLevelButton : TargetColorMatchAreaButton
    {
        [SerializeField] private bool m_IsRestart;
        // protected override void OnClickAction()
        // {
        //     //if (!m_IsRestart)
        //     //     GameManager.Instance.PlayerManager.Player.SetPlayerLevel(GameManager.Instance.PlayerManager.Player.PlayerLevel + 1);
        //     // GameManager.Instance.ResetToMainMenu();
        // }
    }
}
