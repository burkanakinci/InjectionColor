using UnityEngine;
using System.Collections.Generic;
using System;
using Game.Manager;
using Game.Object;

namespace Game.UI
{
    public class UIPanel : CustomBehaviour
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;
        public CanvasGroup CanvasGroup => m_CanvasGroup;
        [SerializeField] protected List<UIArea> m_PanelAreas;
        public UIArea CurrentArea { get; private set; }
        private UIManager m_UIManager;

        public override void Initialize()
        {
            m_UIManager = GameManager.Instance.GetManager<UIManager>();
            m_PanelAreas.ForEach(_area =>
            {
                _area.gameObject.SetActive(true);
                _area.Initialize(this);
            });
        }

        public virtual void ShowPanel()
        {
            m_UIManager.HideAllPanels();

            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }

            CanvasGroup.Open();
            ShowThisPanel();
        }

        public virtual void HidePanel()
        {
            CanvasGroup.Close();
        }

        public virtual void ShowThisPanel()
        {
            m_UIManager.SetCurrentUIPanel(this);
        }

        public virtual void HideAllArea()
        {
            m_PanelAreas.ForEach(_area => { _area.HideArea(); });
        }

        public virtual void ShowAllArea()
        {
            m_PanelAreas.ForEach(_area => { _area.ShowArea(); });
        }

        public virtual void ShowArea<T>(T _area) where T : Enum
        {
            CurrentArea = m_PanelAreas[(int)(object)_area];
            CurrentArea.ShowArea();
        }

        public UIArea GetArea<T>(T _area) where T : Enum
        {
            return m_PanelAreas[(int)(object)_area];
        }

        public T GetArea<T, TEnum>(TEnum _areaType) where T : UIArea where TEnum : Enum
        {
            if (m_PanelAreas[(int)(object)_areaType] is T area)
            {
                return area;
            }

            return null;
        }
    }
}
