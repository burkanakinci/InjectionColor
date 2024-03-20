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
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMinBackDilationStartValue;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMaxBackDilationStartValue;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMinBackDilationValue;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMaxBackDilationValue;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMinBackDilationDuration;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMaxBackDilationDuration;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private Ease m_OnShrinkingBackDilationEase;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private bool m_UseBackDilation;

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
        public float OnShrinkingBackDilationStartValue => Random.Range(m_OnShrinkingMinBackDilationStartValue,m_OnShrinkingMaxBackDilationStartValue);
        public float OnShrinkingBackDilationValue => Random.Range(m_OnShrinkingMinBackDilationValue,m_OnShrinkingMaxBackDilationValue);
        public float OnShrinkingBackDilationDuration => Random.Range(m_OnShrinkingMinBackDilationDuration,m_OnShrinkingMaxBackDilationDuration);
        public Ease OnShrinkingBackDilationEase => m_OnShrinkingBackDilationEase;
        public bool UseBackDilation => m_UseBackDilation;

        #endregion
        
        #region On Change Colorless
        
        public float OnChangeColorlessDuration => m_OnChangeColorlessDuration;
        public Ease OnChangeColorlessEase => m_OnChangeColorlessEase;
        
        #endregion

        #endregion
    }
}