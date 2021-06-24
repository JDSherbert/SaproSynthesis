using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Joshua "JDSherbert" Herbert
/// for SpecialFX Gamejam.
/// https://www.justgiving.com/fundraising/levelupuk
/// </summary>

public class JDH_RigidbodySounds : MonoBehaviour
{
    [System.Serializable]
    public class Parameters
    {
        public float volume = 0.7f;
    }

    [System.Serializable]
    public class Payload
    {
        public AudioSource audiosource;
        public AudioClip audioclip;
        public Collider collider;
    }

    public Parameters parameter = new Parameters();
    public Payload payload = new Payload();


    public void Awake()
    {
        payload.audiosource = GetComponent<AudioSource>();
        payload.collider = GetComponent<Collider>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        payload.audiosource.PlayOneShot(payload.audioclip, parameter.volume);
    }
}
