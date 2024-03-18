using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "SyringeData", menuName = "Syringe Data")]
    public class SyringeData : ScriptableObject
    {
        #region Datas

        #region On Syringe Inside Movement

        [Header("On Syringe Inside Movement")]
        [FoldoutGroup("On Syringe Inside Movement")][SerializeField] 
        private float m_SyringeUpperMovementDuration;
        [FoldoutGroup("On Syringe Inside Movement")][SerializeField] 
        private float m_SyringeUpperMovementEase;

        #endregion

        #region On Syringe Movement To Colored

        [Header("On Syringe Movement To Colored")] 
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField][Range(0.0f,1.0f)]
        private float m_OnMoveToColoredTargetDistanceMultiply;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredJumpPower;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredJumpDuration;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private Ease m_OnMoveToColoredJumpEase;

        #endregion

        #region On Deinject Shaking

        [Header("On Deinject Shaking")] 
        [SerializeField] private DeinjectShakingPair m_DeinjectShakingPair;

        #endregion

        #region On Deinject Shaking Back

        [Header("On Deinject Shaking Back")] 
        [SerializeField] private DeinjectShakingBackPair m_DeinjectShakingBackPair;

        #endregion

        #endregion

        #region ExternalAccess

        #region On Syringe Inside Movement

        public float SyringeUpperMovementDuration => m_SyringeUpperMovementDuration;
        public float SyringeUpperMovementEase => m_SyringeUpperMovementEase;

        #endregion

        #region On Syringe Movement To Colored
        
        public float OnMoveToColoredTargetDistanceMultiply => m_OnMoveToColoredTargetDistanceMultiply;
        public float OnMoveToColoredJumpPower => m_OnMoveToColoredJumpPower;
        public float OnMoveToColoredJumpDuration => m_OnMoveToColoredJumpDuration;
        public Ease OnMoveToColoredJumpEase => m_OnMoveToColoredJumpEase;

        #endregion
        
        #region On Deinject Shaking

        public DeinjectShakingPair DeinjectShakingPair => m_DeinjectShakingPair;  

        #endregion
        
        #region On Deinject Shaking Back
        
        public DeinjectShakingBackPair DeinjectShakingBackPair => m_DeinjectShakingBackPair;

        #endregion

        #endregion
    }
    [Serializable]
    public struct DeinjectShakingPair
    {
        #region Datas

        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMinDuration;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMaxDuration;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMinStrength;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMaxStrength;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private int m_OnDeinjectShakeMinVibration;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private int m_OnDeinjectShakeMaxVibration;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMinRandomness;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private float m_OnDeinjectShakeMaxRandomness;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private bool  m_OnDeinjectShakeIsFadeOut;
        [FoldoutGroup("On Deinject Shaking")] [SerializeField] 
        private Ease  m_OnDeinjectShakeEase;

        #endregion

        #region ExternalAccess

        public float m_OnDeinjectShakeDuration => Random.Range(m_OnDeinjectShakeMinDuration,m_OnDeinjectShakeMaxDuration);
        public float m_OnDeinjectShakeStrength => Random.Range(m_OnDeinjectShakeMinStrength,m_OnDeinjectShakeMaxStrength);
        public int m_OnDeinjectShakeVibration => Random.Range(m_OnDeinjectShakeMinVibration,m_OnDeinjectShakeMaxVibration);
        public float m_OnDeinjectShakeRandomness => Random.Range(m_OnDeinjectShakeMinRandomness,m_OnDeinjectShakeMaxRandomness);
        public bool  OnDeinjectShakeIsFadeOut => m_OnDeinjectShakeIsFadeOut;
        public Ease  OnDeinjectShakeEase => m_OnDeinjectShakeEase;

        #endregion
    }
    [Serializable]
    public struct DeinjectShakingBackPair
    {
        #region Datas

        [SerializeField] private float m_OnDeinjectShakeBackMinDuration;
        [SerializeField] private float m_OnDeinjectShakeBackMaxDuration;
        [SerializeField] private Ease  m_OnDeinjectShakeBackEase;

        #endregion

        #region ExternalAccess

        public float OnDeinjectShakeBackDuration => Random.Range(m_OnDeinjectShakeBackMinDuration,m_OnDeinjectShakeBackMaxDuration);
        public Ease  OnDeinjectShakeBackEase => m_OnDeinjectShakeBackEase;

        #endregion
    }
}