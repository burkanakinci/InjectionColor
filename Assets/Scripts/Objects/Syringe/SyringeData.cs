using UnityEngine;

namespace Game.Object
{
    [CreateAssetMenu(fileName = "SyringeData", menuName = "Syringe Data")]
    public class SyringeData : ScriptableObject
    {
        #region Datas
        
        [SerializeField] private float m_SyringeUpperMovementDuration;
        [SerializeField] private float m_SyringeUpperMovementEase;

        #endregion

        #region ExternalAccess

        public float SyringeUpperMovementDuration => m_SyringeUpperMovementDuration;
        public float SyringeUpperMovementEase => m_SyringeUpperMovementEase;

        #endregion
    }
}