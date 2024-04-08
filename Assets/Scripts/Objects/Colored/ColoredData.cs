using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "ColoredData", menuName = "Colored Data")]
    public class ColoredData : ScriptableObject
    {
        #region Datas

        #region On Set Splash VFX Enabled

        [Header("On Set Splash VFX Enabled")] 
        [FoldoutGroup("On Set Splash VFX Enabled")] [SerializeField]
        private OnEnabledSplashVFXPair m_OnEnabledSplashVFXPair;
        [Header("On Set Splash VFX Enabled")] 
        [FoldoutGroup("On Set Splash VFX Enabled")] [SerializeField]
        private OnDisabledSplashVFXPair m_OnDisabledSplashVFXPair;


        #endregion
        
        #region On Change Colorful Jump

        [Header("On Change Colorful Jump")]
        [FoldoutGroup("On Change Colorful Jump")][SerializeField]
        private float m_OnChangeColorfulJumpDuration;
        [FoldoutGroup("On Change Colorful Jump")][SerializeField] 
        private Ease m_OnChangeColorfulJumpEase;
        [FoldoutGroup("On Change Colorful Jump")][SerializeField]
        private float m_OnChangeColorfulJumpBackDuration;
        [FoldoutGroup("On Change Colorful Jump")][SerializeField] 
        private Ease m_OnChangeColorfulJumpBackEase;

        #endregion

        #endregion

        #region ExternalAccess

        #region On Set Splash VFX Enabled

        public OnEnabledSplashVFXPair OnEnabledSplashVFXPair => m_OnEnabledSplashVFXPair;
        public OnDisabledSplashVFXPair OnDisabledSplashVFXPair => m_OnDisabledSplashVFXPair;

        #endregion

        #endregion
        
        #region On Change Colorful Jump

        public float OnChangeColorfulJumpDuration => m_OnChangeColorfulJumpDuration;
        public Ease OnChangeColorfulJumpEase => m_OnChangeColorfulJumpEase;
        public float OnChangeColorfulJumpBackDuration => m_OnChangeColorfulJumpBackDuration;
        public Ease OnChangeColorfulJumpBackEase => m_OnChangeColorfulJumpBackEase;

        #endregion
    }

    [Serializable]
    public struct OnEnabledSplashVFXPair
    {
        #region Datas
        [SerializeField]
        private float m_EnabledSimulationSpeed;
        [SerializeField]
        private float m_EnabledSimulationDuration;
        [SerializeField]
        private Ease m_EnabledSimulationEase;
        

        #endregion

        #region External Access

        public float EnabledSimulationSpeed => m_EnabledSimulationSpeed;
        public float EnabledSimulationDuration => m_EnabledSimulationDuration;
        public Ease EnabledSimulationEase => m_EnabledSimulationEase;

        #endregion
    }
    [Serializable]
    public struct OnDisabledSplashVFXPair
    {
        #region Datas
        [SerializeField]
        private float m_DisabledSimulationDuration;
        [SerializeField]
        private Ease m_DisabledSimulationEase;
        

        #endregion

        #region External Access

        public float DisabledSimulationDuration => m_DisabledSimulationDuration;
        public Ease DisabledSimulationEase => m_DisabledSimulationEase;

        #endregion
    }
}