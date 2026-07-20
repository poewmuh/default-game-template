using UnityEngine;

namespace Game.Application.Audio
{
    public class UnityAudioSystem : MonoBehaviour, IAudioSystem
    {
        private bool _warningShown;
        
        void IAudioSystem.PlayOneShot(string key)
        {
            WarnNotImplemented();
        }

        void IAudioSystem.PlayOneShot(string key, Vector3 worldPosition)
        {
            WarnNotImplemented();
        }

        void IAudioSystem.PlayMusic(string key)
        {
            WarnNotImplemented();
        }

        void IAudioSystem.StopMusic()
        {
            WarnNotImplemented();
        }

        void IAudioSystem.PlayAmbient(string key)
        {
            WarnNotImplemented();
        }

        void IAudioSystem.StopAmbient(string key)
        {
            WarnNotImplemented();
        }

        void IAudioSystem.StopAllAmbient()
        {
            WarnNotImplemented();
        }

        private void WarnNotImplemented()
        {
            if (_warningShown)
                return;

            _warningShown = true;
            Debug.LogWarning("[UnityAudioSystem] Audio system is registered, but playback is not implemented yet.");
        }
    }
}
