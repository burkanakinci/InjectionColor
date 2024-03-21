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

        #endregion

        #region ExternalAccess

        #region On Change Liquid Color

        public ChangePouringLiquidColorPair ChangePouringLiquidColorPair => m_ChangePouringLiquidColorPair;

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
}