using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "ShrinkingPieceData", menuName = "Shrinking Piece Data")]
    public class ShrinkingPieceData : ScriptableObject
    {
        #region Datas

        #region On Shrinking

        [Header("On Shrinking")]
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingDuration;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private Ease m_OnShrinkingEase;

        #endregion

        #region On Change Colorless

        [Header("On Change Colorless")]
        [FoldoutGroup("On Change Colorless")][SerializeField] 
        private float m_OnChangeColorlessDuration;
        [FoldoutGroup("On Change Colorless")][SerializeField] 
        private Ease m_OnChangeColorlessEase;


        #endregion

        #endregion

        #region ExternalAccess
        
        #region On Shrinking
        
        public float OnShrinkingDuration => m_OnShrinkingDuration;
        public Ease OnShrinkingEase => m_OnShrinkingEase;
        
        #endregion
        
        #region On Change Colorless
        
        public float OnChangeColorlessDuration => m_OnChangeColorlessDuration;
        public Ease OnChangeColorlessEase => m_OnChangeColorlessEase;
        
        #endregion

        #endregion
    }
}