using System.Collections.Generic;
using Game.UI;
using Game.Utilities.Constants;
using UnityEngine;

namespace Game.Manager
{
    public class UIManager : IManager
    {
        #region Fields

        public UIPanel CurrentUIPanel { get; private set; }
        [SerializeField] private List<UIPanel> m_UIPanels;

        #endregion

        public void InitializeManager()
        {
            m_UIPanels.ForEach(_panel =>
            {
                _panel.Initialize();
                _panel.gameObject.SetActive(true);
            });
        }

        public void HideAllPanels()
        {
            m_UIPanels.ForEach(x =>
            {
                x.HidePanel();
            });
        }

        #region Getter

        public UIPanel GetPanel(UIPanelType _panel)
        {
            return m_UIPanels[(int)_panel];
        }

        #endregion

        #region Setter

        public void SetCurrentUIPanel(UIPanel _panel)
        {
            CurrentUIPanel = _panel;
        }

        public void SetCurrentUIPanel(UIPanelType _panel)
        {
            CurrentUIPanel = m_UIPanels[(int)_panel];
        }

        #endregion
    }
}
