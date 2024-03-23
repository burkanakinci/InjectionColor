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
        private float m_OnShrinkingMinStartDelay;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMaxStartDelay;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMinDuration;
        [FoldoutGroup("On Shrinking")][SerializeField] 
        private float m_OnShrinkingMaxDuration;
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
        private bool m_OnShrinkingUseBackDilation;

        #endregion

        #region On Change Colorless

        [Header("On Change Colorless")]
        [FoldoutGroup("On Change Colorless")][SerializeField] 
        private float m_OnChangeColorlessDuration;
        [FoldoutGroup("On Change Colorless")][SerializeField] 
        private Ease m_OnChangeColorlessEase;


        #endregion

        #region On Dilation

        [Header("On Dilation")]
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMinStartDelay;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMaxStartDelay;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMinDuration;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMaxDuration;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private Ease m_OnDilationEase;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMinBackDilationStartValue;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMaxBackDilationStartValue;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMinBackDilationValue;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMaxBackDilationValue;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMinBackDilationDuration;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private float m_OnDilationMaxBackDilationDuration;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private Ease m_OnDilationBackDilationEase;
        [FoldoutGroup("On Dilation")][SerializeField] 
        private bool m_OnDilationUseBackDilation;

        #endregion
        
        #region On Change Coloful

        [Header("On Change Coloful")]
        [FoldoutGroup("On Change Coloful")][SerializeField]
        private float m_OnChangeColofulDuration;
        [FoldoutGroup("On Change Coloful")][SerializeField] 
        private Ease m_OnChangeColofulEase;


        #endregion

        #endregion

        #region ExternalAccess
        
        #region On Shrinking

        public float OnShrinkingStartDelay => Random.Range(m_OnShrinkingMinStartDelay, m_OnShrinkingMaxStartDelay);
        public float OnShrinkingDuration => Random.Range(m_OnShrinkingMinDuration,m_OnShrinkingMaxDuration);
        public Ease OnShrinkingEase => m_OnShrinkingEase;
        public float OnShrinkingBackDilationStartValue => Random.Range(m_OnShrinkingMinBackDilationStartValue,m_OnShrinkingMaxBackDilationStartValue);
        public float OnShrinkingBackDilationValue => Random.Range(m_OnShrinkingMinBackDilationValue,m_OnShrinkingMaxBackDilationValue);
        public float OnShrinkingBackDilationDuration => Random.Range(m_OnShrinkingMinBackDilationDuration,m_OnShrinkingMaxBackDilationDuration);
        public Ease OnShrinkingBackDilationEase => m_OnShrinkingBackDilationEase;
        public bool OnShrinkingUseBackDilation => m_OnShrinkingUseBackDilation;

        #endregion
        
        #region On Change Colorless
        
        public float OnChangeColorlessDuration => m_OnChangeColorlessDuration;
        public Ease OnChangeColorlessEase => m_OnChangeColorlessEase;
        
        #endregion

        #region On Dilation
        
        public float OnDilationStartDelay => Random.Range(m_OnDilationMinStartDelay,m_OnDilationMaxStartDelay);
        public float OnDilationDuration => Random.Range(m_OnDilationMinDuration,m_OnDilationMaxDuration);
        public Ease OnDilationEase => m_OnDilationEase;
        public float OnDilationBackDilationStartValue => Random.Range(m_OnDilationMinBackDilationStartValue,m_OnDilationMaxBackDilationStartValue);
        public float OnDilationBackDilationValue => Random.Range(m_OnDilationMinBackDilationValue,m_OnDilationMaxBackDilationValue);
        public float OnDilationBackDilationDuration => Random.Range(m_OnDilationMinBackDilationDuration,m_OnDilationMaxBackDilationDuration);
        public Ease OnDilationBackDilationEase => m_OnDilationBackDilationEase;
        public bool OnDilationUseBackDilation => m_OnDilationUseBackDilation;

        #endregion

        #region On Change Coloful

        public float OnChangeColofulDuration => m_OnChangeColofulDuration;
        public Ease OnChangeColofulEase => m_OnChangeColofulEase;
        
        #endregion

        #endregion
    }
}