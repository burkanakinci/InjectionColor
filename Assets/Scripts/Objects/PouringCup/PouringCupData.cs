using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "PouringCupData", menuName = "Pouring Cup Data")]
    public class PouringCupData : ScriptableObject
    {
        #region Datas

        #region On Change Liquid Color

        [Header("On Change Liquid Color")] 
        [FoldoutGroup("On Change Liquid Color")] [SerializeField]
        private ChangePouringLiquidColorPair m_ChangePouringLiquidColorPair;

        #endregion

        #region On Down Liquid Normal Speed 

        [Header("On Down Liquid Normal Speed")] 
        [FoldoutGroup("On Down Liquid Normal Speed")] [SerializeField]
        private ChangeNormalSpeedPourLiquidPair m_DownNormalSpeedPourLiquidPair;

        #endregion
        
        #region On Up Liquid Normal Speed 

        [Header("On Up Liquid Normal Speed ")] 
        [FoldoutGroup("On Up Liquid Normal Speed ")] [SerializeField]
        private ChangeNormalSpeedPourLiquidPair m_UpNormalSpeedPourLiquidPair;

        #endregion

        #endregion

        #region ExternalAccess

        #region On Change Liquid Color

        public ChangePouringLiquidColorPair ChangePouringLiquidColorPair => m_ChangePouringLiquidColorPair;

        #endregion

        #region On Down Liquid Normal Speed 
        
        public ChangeNormalSpeedPourLiquidPair DownNormalSpeedPourLiquidPair => m_DownNormalSpeedPourLiquidPair;

        #endregion
        
        #region On Up Liquid Normal Speed 
        
        public ChangeNormalSpeedPourLiquidPair UpNormalSpeedPourLiquidPair => m_UpNormalSpeedPourLiquidPair;

        #endregion

        #endregion
    }

    [Serializable]
    public struct ChangePouringLiquidColorPair
    {
        #region Datas

        [Header("On Change Liquid Color")] 
        [FoldoutGroup("On Change Liquid Color")] [SerializeField] 
        private float m_ChangeLiquidDuration;
        [FoldoutGroup("On Change Liquid Color")] [SerializeField] 
        private Ease m_ChangeLiquidEase;

        #endregion

        #region ExternalAccess

        public float ChangeLiquidDuration => m_ChangeLiquidDuration;
        public Ease ChangeLiquidEase => m_ChangeLiquidEase;

        #endregion
    }

    [Serializable]
    public struct ChangeNormalSpeedPourLiquidPair
    {
        #region Datas

        [SerializeField] 
        private ChangeNormalSpeedPourLiquidType m_OnChangeNormalSpeedType;
        [SerializeField] 
        private float m_OnChangeNormalSpeedDuration;
        [SerializeField] 
        private Vector2 m_OnChangeNormalSpeedTarget;
        [SerializeField] 
        private Ease m_OnChangeNormalSpeedEase;

        #endregion

        #region ExternalAccess

        public ChangeNormalSpeedPourLiquidType OnChangeNormalSpeedType => m_OnChangeNormalSpeedType;
        public float OnChangeNormalSpeedDuration => m_OnChangeNormalSpeedDuration;
        public Ease OnChangeNormalSpeedEase => m_OnChangeNormalSpeedEase;
        public Vector2 OnChangeNormalSpeedTarget => m_OnChangeNormalSpeedTarget;

        #endregion
    }

    public enum ChangeNormalSpeedPourLiquidType
    {
        OnNormalSpeedDown = 0,
        OnNormalSpeedUp = 1,
    }
}