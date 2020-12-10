using UnityEngine.Audio;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace BlackneyStudios.GuiWidget
{
    [Serializable]
    public class AudioModel
    {
        [HorizontalGroup("General Properties", 75)]
        [HideLabel]
        [PreviewField(75)]
        [Tooltip("The sound file that will be played.")]
        public AudioClip audioClip;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Tooltip("When checked, the volume of this sound will be played at a random value between a range of volumes you specify.")]
        public bool randomizeVolume;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0f, 1f)]
        [ShowIf("ShowRandomVolumeSettings")]
        [Tooltip("The lowest level of volume the sound can be played at randomly.")]
        public float randomVolumeLowerLimit = 0.01f;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0f, 1f)]
        [ShowIf("ShowRandomVolumeSettings")]
        [Tooltip("The highest level of volume the sound can be played at randomly.")]
        public float randomVolumeUpperLimit = 1f;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0f, 1f)]
        [ShowIf("ShowVolume")]
        [Tooltip("The volume of the sound being played.")]
        public float volume = 0.5f;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Tooltip("When checked, the pitch of this event will be a random value between a range of pitches you specify.")]
        public bool randomizePitch;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0.1f, 3f)]
        [ShowIf("ShowRandomPitchSettings")]
        [Tooltip("The lowest pitch the sound can be played at randomly.")]
        public float randomPitchLowerLimit = 0.01f;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0.1f, 3f)]
        [ShowIf("ShowRandomPitchSettings")]
        [Tooltip("The highest pitch the sound can be played at randomly.")]
        public float randomPitchUpperLimit = 10f;

        [VerticalGroup("General Properties/Stats")]
        [LabelWidth(100)]
        [Range(0.1f, 3f)]
        [ShowIf("ShowPitch")]
        [Tooltip("The pitch of the sound being played (NOTE: a value of 1 will mean the sound will play at its normal pitch).")]
        public float pitch = 1f;

        // Misc
        [HideInInspector] public bool fadingIn;
        [HideInInspector] public bool fadingOut;


        public bool ShowVolume()
        {
            return randomizeVolume == false;
        }
        public bool ShowRandomVolumeSettings()
        {
            return randomizeVolume == true;
        }

        public bool ShowPitch()
        {
            return randomizePitch == false;
        }
        public bool ShowRandomPitchSettings()
        {
            return randomizePitch == true;
        }



    }
}


