using UnityEngine;

namespace RagdollWakeUp.Utilities {
    [RequireComponent (typeof (AudioSource))]
    public class PlaySoundOnCollision : MonoBehaviour {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private float forceThreshold;

        private AudioSource source;

        private void Awake () {
            source = GetComponent<AudioSource> ();
        }

        private void OnCollisionEnter (Collision other) {
            if (other.relativeVelocity.sqrMagnitude >= forceThreshold * forceThreshold) {
                source.clip = clips[Random.Range (0, clips.Length)];
                source.Play ();
            }
        }
    }
}