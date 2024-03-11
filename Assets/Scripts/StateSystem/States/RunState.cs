﻿using UnityEngine;
using System;
public class RunState : IPlayerState
{
    public Action OnEnterEvent { get; set; }
    public Action OnExitEvent { get; set; }
    private Player m_Player;
    public RunState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel).ShowPanel();
        OnEnterEvent?.Invoke();
    }
    public void UpdateLogic()
    {
        GameManager.Instance.InputManager.UpdateInput();
    }
    public void UpdatePhysic()
    {
    }
    public void Exit()
    {
        OnExitEvent?.Invoke();
    }
    public void TriggerEnter(Collider _other)
    {
    }
}
