using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Manager
{
    public class AudioManager : IManager
    {
        [SerializeField] 
        private AudioPair[] m_Audios;
        [SerializeField] 
        private AudioSourcePair[] m_AudioSources;
        public void InitializeManager()
        {
            for (int i = 0; i < m_AudioSources.Length; i++)
            {
                m_AudioSources[i].AudioSource.Stop();
            }
        }
        [Button]
        public void Play(AudioType _audioType)
        {
            m_AudioSources[(int)m_Audios[(int)_audioType].AudioSourceType].AudioSource.Stop();
            m_AudioSources[(int)m_Audios[(int)_audioType].AudioSourceType].AudioSource.clip = m_Audios[(int)_audioType].AudioClip;
            m_AudioSources[(int)m_Audios[(int)_audioType].AudioSourceType].AudioSource.Play();
        }

        [Serializable]
        public struct AudioPair
        {
            #region Datas

            [SerializeField] 
            private AudioClip m_AudioClip;
            [SerializeField] 
            private AudioType m_AudioType;
            [SerializeField] 
            private AudioSourceType m_AudioSourceType;

            #endregion

            #region External Access

            public AudioClip AudioClip => m_AudioClip;
            public AudioType AudioType => m_AudioType;
            public AudioSourceType AudioSourceType => m_AudioSourceType;

            #endregion
        }
        [Serializable]
        public struct  AudioSourcePair
        {
            #region Datas

            [SerializeField] 
            private AudioSource m_AudioSource;
            [SerializeField] 
            private AudioSourceType m_AudioSourceType;

            #endregion

            #region External Access

            public AudioSource AudioSource => m_AudioSource;
            public AudioSourceType AudioSourceType => m_AudioSourceType;

            #endregion
        }
        public enum AudioSourceType
        {
            Camera = 0,
            Syringe = 1,
        }
    }

    public enum AudioType
    {
        SyringeStab = 0,
        SyringeMoveToColored = 1,
        SyringeBackFromColored = 2,
        SyringePouringLiquid = 3,
    }
}
