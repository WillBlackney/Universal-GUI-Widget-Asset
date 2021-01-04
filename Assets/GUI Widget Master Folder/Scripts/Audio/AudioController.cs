using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace BlackneyStudios.GuiWidget
{
    public class AudioController : Singleton<AudioController>
    {
        [Header("Components + Pooling")]
        [SerializeField] private GameObject audioPlayerPrefab;
        [SerializeField] private Transform audioPlayerPoolParent;
        [SerializeField] private List<AudioPlayer> audioPlayerPool;

        public void HandlePlayAudio(AudioModel audioData)
        {
            // Find an available audio player from the pool, or create a new one
            AudioPlayer player = GetNextAvailableAudioPlayer();

            // Set up the audio player's settings from the provided data
            BuildAudioPlayerFromAudioModelData(audioData, player);

            // Play the sound!
            player.source.Play();
        }

        private void BuildAudioPlayerFromAudioModelData(AudioModel data, AudioPlayer player)
        {
            // Get the actual audio clip to be played
            player.source.clip = data.audioClip;

            // Randomize pitch if marked to do so
            if (data.randomizePitch)
                player.source.pitch = RandomGenerator.NumberBetween(data.randomPitchLowerLimit, data.randomPitchUpperLimit);

            // Otherwise, just assign the user's set pitch setting
            else
                player.source.pitch = data.pitch;
            
            // Randomize volume if marked to do so
            if (data.randomizeVolume)            
                player.source.volume = RandomGenerator.NumberBetween(data.randomVolumeLowerLimit, data.randomVolumeUpperLimit);

            // Otherwise, just assign the user's set volume level
            else
                player.source.volume = data.volume;            
        }

        // Audio Player + Pooling Logic
        #region
        private AudioPlayer GetNextAvailableAudioPlayer()
        {
            AudioPlayer availablePlayer = null;

            // Find an available player
            foreach (AudioPlayer ap in audioPlayerPool)
            {
                if (ap.source.isPlaying == false)
                {
                    Debug.LogWarning("GetNextAvailableAudioPlayer() found an available player, returning it");
                    availablePlayer = ap;
                    break;
                }
            }

            // If there arent any available, create new one, add it to pool, then use it
            if (availablePlayer == null)
            {
                Debug.LogWarning("GetNextAvailableAudioPlayer() couldn't find an available player, creating a new one");
                availablePlayer = CreateAndAddAudioPlayerToPool();
            }

            return availablePlayer;
        }
        private AudioPlayer CreateAndAddAudioPlayerToPool()
        {
            // Create an audio player from prefab
            AudioPlayer newAP = Instantiate(audioPlayerPrefab, audioPlayerPoolParent).GetComponent<AudioPlayer>();

            // Add it to the audio pool list
            audioPlayerPool.Add(newAP);
            return newAP;
        }

        #endregion

    }
}

