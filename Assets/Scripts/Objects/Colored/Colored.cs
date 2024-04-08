using DG.Tweening;
using UnityEngine;

namespace Game.Object
{
    public class Colored : CustomBehaviour
    {
        #region Fields

        [SerializeField] private ColoredVisual m_ColoredVisual;
        [SerializeField] private ColoredData m_ColoredData;
        [SerializeField] private Transform m_SyringeParent;
        [SerializeField] private Color m_ObjectColor;
        [SerializeField] private Vector3 m_SyringeParentShrinkEndPos;
        [SerializeField] private Vector3 m_OnChangeColorfulJumpPos;
        private Vector3 m_SyringeParentShrinkStartPos;
        public Color ObjectColor => m_ObjectColor;

        #endregion

        #region ExternalAccess

        public Transform SyringeTargetParent => m_SyringeParent;

        #endregion
        public override void Initialize()
        {
            m_ColoredVisual.Initialize(this);
            m_SyringeParentShrinkStartPos = m_SyringeParent.transform.localPosition;
        }

        public void OnChangeShrinkingValue(float _lerp)
        {
            SyringeTargetParent.localPosition = Vector3.Lerp(m_SyringeParentShrinkStartPos,m_SyringeParentShrinkEndPos,_lerp);
        }

        public void DeinjectColor()
        {
            SetSplashVFXEnabled(true);
            SetFirstSplashVFXEnabled(true);
            m_ColoredVisual.DeinjectColoredVisual();
        }

        public void DilationColored()
        {
            m_ColoredVisual.DilationColoredVisual();
        }

        public void OnChangeColoredJump()
        {
            m_ColoredVisual.LocalMoveTween(m_OnChangeColorfulJumpPos,m_ColoredData.OnChangeColorfulJumpDuration)
                .SetEase(m_ColoredData.OnChangeColorfulJumpEase)
                .OnComplete(() =>
                {
                    m_ColoredVisual
                        .LocalMoveTween(Vector3.zero, m_ColoredData.OnChangeColorfulJumpBackDuration)
                        .SetEase(m_ColoredData.OnChangeColorfulJumpBackEase);
                });
        }

        public void SetSplashVFXEnabled(bool _isEnable)
        {
            if (_isEnable)
            {
                m_ColoredVisual.SetSplashVFXEnabled(m_ColoredData.OnEnabledSplashVFXPair);
            }
            else
            {
                m_ColoredVisual.SetSplashVFXEnabled(m_ColoredData.OnDisabledSplashVFXPair);
            }
        }
        public void SetFirstSplashVFXEnabled(bool _isEnable)
        {
            m_ColoredVisual.SetFirstSplashVFXEnabled(_isEnable);
        }
    }
}
