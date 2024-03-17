using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "ShrinkingPieceData", menuName = "Shrinking Piece Data")]
    public class ShrinkingPieceData : ScriptableObject
    {
        #region Datas
        
        [Header("On Shrinking")]
        [FoldoutGroup("On Shrinking")][SerializeField] private float m_OnShrinkingDuration;
        [FoldoutGroup("On Shrinking")][SerializeField] private Ease m_OnShrinkingEase;

        #endregion

        #region ExternalAccess
        
        public float OnShrinkingDuration => m_OnShrinkingDuration;
        public Ease OnShrinkingEase => m_OnShrinkingEase;

        #endregion
    }
}