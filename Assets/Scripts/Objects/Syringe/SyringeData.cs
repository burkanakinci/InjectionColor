using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

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

        #endregion
    }
}