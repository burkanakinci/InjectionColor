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
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredJumpFirstHeight;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredJumpPower;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredJumpDuration;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private Ease m_OnMoveToColoredJumpEase;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredRotateDuration;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private Ease m_OnMoveToColoredRotateEase;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private MovementToColoredFlipPair m_MovementToColoredFlipPair;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private float m_OnMoveToColoredSettleDuration;
        [FoldoutGroup("On Syringe Movement To Colored")] [SerializeField]
        private Ease m_OnMoveToColoredSettleEase;

        #endregion

        #region On Deinject Shaking

        [Header("On Deinject Shaking")] 
        [FoldoutGroup("On Deinject Shaking")][SerializeField] 
        private DeinjectShakingPair m_DeinjectShakingPair;

        #endregion

        #region On Deinject Shaking Back

        [Header("On Deinject Shaking Back")] 
        [FoldoutGroup("On Deinject Shaking Back")][SerializeField] 
        private DeinjectShakingBackPair m_DeinjectShakingBackPair;

        #endregion

        #region On Syringe Upper Movement Up

        [Header("On Syringe Upper Movement Up")] 
        [FoldoutGroup("On Syringe Upper Movement Up")][SerializeField] 
        private DeinjectMovementUpPair m_DeinjectMovementUpPair;

        #endregion
        
        #region On Syringe Upper Movement Down

        [Header("On Syringe Upper Movement Down")] 
        [FoldoutGroup("On Syringe Upper Movement Down")][SerializeField] 
        private DeinjectMovementDownPair m_DeinjectMovementDownPair;

        #endregion
        
        #region On Liquid Up Pair

        [Header("On Liquid Up Pair")] 
        [FoldoutGroup("On Liquid Up Pair")][SerializeField] 
        private DeinjectLiquidUpPair m_DeinjectLiquidUpPair;

        #endregion
        
        #region On Liquid Down Pair

        [Header("On Liquid Down Pair")] 
        [FoldoutGroup("On Liquid Down Pair")][SerializeField] 
        private DeinjectLiquidDownPair m_DeinjectLiquidDownPair;

        #endregion

        #region On Syringe Pouring Movement

        [Header("On Syringe Pouring Movement")] 
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private float m_OnSyringePourMovementStartDelay;
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private float m_OnSyringePourMovementJumpPower;
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private float m_OnSyringePourMovementJumpDuration;
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private Ease m_OnSyringePourMovementJumpEase;
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private float m_OnSyringePourMovementRotateDuration;
        [FoldoutGroup("On Syringe Pouring Movement")][SerializeField] 
        private Ease m_OnSyringePourMovementRotateEase;

        #endregion
        
        #region On Syringe Completed Pouring

        [Header("On Syringe Completed Pouring")] 
        [FoldoutGroup("On Syringe Completed Pouring")][SerializeField] 
        private float m_OnSyringeCompletedPouringStartDelay;
        [FoldoutGroup("On Syringe Completed Pouring")][SerializeField] 
        private float m_OnSyringeCompletedPouringJumpPower;
        [FoldoutGroup("On Syringe Completed Pouring")][SerializeField] 
        private float m_OnSyringeCompletedPouringJumpDuration;
        [FoldoutGroup("On Syringe Completed Pouring")][SerializeField] 
        private Ease m_OnSyringeCompletedPouringJumpEase;

        #endregion

        #endregion

        #region ExternalAccess

        #region On Syringe Inside Movement

        public float SyringeUpperMovementDuration => m_SyringeUpperMovementDuration;
        public float SyringeUpperMovementEase => m_SyringeUpperMovementEase;

        #endregion

        #region On Syringe Movement To Colored

        public float OnMoveToColoredJumpFirstHeight => m_OnMoveToColoredJumpFirstHeight;
        public float OnMoveToColoredJumpPower => m_OnMoveToColoredJumpPower;
        public float OnMoveToColoredJumpDuration => m_OnMoveToColoredJumpDuration;
        public Ease OnMoveToColoredJumpEase => m_OnMoveToColoredJumpEase;
        public float OnMoveToColoredRotateDuration => m_OnMoveToColoredRotateDuration;
        public Ease OnMoveToColoredRotateEase => m_OnMoveToColoredRotateEase;
        public MovementToColoredFlipPair MovementToColoredFlipPair => m_MovementToColoredFlipPair;
        public float OnMoveToColoredSettleDuration => m_OnMoveToColoredSettleDuration;
        public Ease OnMoveToColoredSettleEase => m_OnMoveToColoredSettleEase;

        #endregion
        
        #region On Deinject Shaking

        public DeinjectShakingPair DeinjectShakingPair => m_DeinjectShakingPair;  

        #endregion
        
        #region On Deinject Shaking Back
        
        public DeinjectShakingBackPair DeinjectShakingBackPair => m_DeinjectShakingBackPair;

        #endregion
        
        #region On Syringe Upper Movement Up
        
        public DeinjectMovementUpPair DeinjectMovementUpPair => m_DeinjectMovementUpPair;

        #endregion

        #region On Syringe Upper Movement Down

        public DeinjectMovementDownPair DeinjectMovementDownPair => m_DeinjectMovementDownPair;

        #endregion
        
        #region On Liquid Up Pair
        
        public DeinjectLiquidUpPair DeinjectLiquidUpPair => m_DeinjectLiquidUpPair;

        #endregion
        
        #region On Liquid Down Pair
        
        public DeinjectLiquidDownPair DeinjectLiquidDownPair => m_DeinjectLiquidDownPair;

        #endregion
        
        #region On Syringe Pouring Movement

        public float OnSyringePourMovementStartDelay => m_OnSyringePourMovementStartDelay;
        public float OnSyringePourMovementJumpPower => m_OnSyringePourMovementJumpPower;
        public float OnSyringePourMovementJumpDuration => m_OnSyringePourMovementJumpDuration;
        public Ease OnSyringePourMovementJumpEase => m_OnSyringePourMovementJumpEase;
        public float OnSyringePourMovementRotateDuration => m_OnSyringePourMovementRotateDuration;
        public Ease OnSyringePourMovementRotateEase => m_OnSyringePourMovementRotateEase;

        #endregion
        
        #region On Syringe Completed Pouring
        
        public float OnSyringeCompletedPouringStartDelay => m_OnSyringeCompletedPouringStartDelay;
        public float OnSyringeCompletedPouringJumpPower => m_OnSyringeCompletedPouringJumpPower;
        public float OnSyringeCompletedPouringJumpDuration => m_OnSyringeCompletedPouringJumpDuration;
        public Ease OnSyringeCompletedPouringJumpEase => m_OnSyringeCompletedPouringJumpEase;

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

    [Serializable]
    public struct MovementToColoredFlipPair
    {
        #region Datas

        [SerializeField] private float m_OnMoveToColoredFlipDuration;
        [SerializeField] private Ease m_OnMoveToColoredFlipEase;

        #endregion

        #region ExternalAccess

        public float OnMoveToColoredFlipDuration => m_OnMoveToColoredFlipDuration;
        public Ease OnMoveToColoredFlipEase => m_OnMoveToColoredFlipEase;

        #endregion
    }
    
    [Serializable]
    public struct DeinjectMovementUpPair
    {
        #region Datas
        
        [SerializeField] private float m_MoveUpDuration;
        [SerializeField] private float m_MoveUpDistance;
        [SerializeField] private Ease m_MoveUpEase;

        #endregion

        #region ExternalAccess
        
        public float MoveUpDuration => m_MoveUpDuration;
        public float MoveUpDistance => m_MoveUpDistance;
        public Ease MoveUpEase => m_MoveUpEase;

        #endregion
    }
    
    [Serializable]
    public struct DeinjectMovementDownPair
    {
        #region Datas

        [SerializeField] private float m_MoveDownStartDelay;
        [SerializeField] private float m_MoveDownDuration;
        [SerializeField] private Ease m_MoveDownEase;

        #endregion

        #region ExternalAccess
        
        public float MoveDownStartDelay => m_MoveDownStartDelay;
        public float MoveDownDuration => m_MoveDownDuration;
        public Ease MoveDownEase => m_MoveDownEase;

        #endregion
    }
    
    [Serializable]
    public struct DeinjectLiquidUpPair
    {
        #region Datas
        
        [SerializeField] private float m_LiquidUpDuration;
        [SerializeField] private Ease m_LiquidUpEase;

        #endregion

        #region ExternalAccess
        
        public float LiquidUpDuration => m_LiquidUpDuration;
        public Ease LiquidUpEase => m_LiquidUpEase;

        #endregion
    }
    [Serializable]
    public struct DeinjectLiquidDownPair
    {
        #region Datas
        
        [SerializeField] private float m_LiquidDownDuration;
        [SerializeField] private Ease m_LiquidDownEase;

        #endregion

        #region ExternalAccess
        
        public float LiquidDownDuration => m_LiquidDownDuration;
        public Ease LiquidDownEase => m_LiquidDownEase;

        #endregion
    }
}