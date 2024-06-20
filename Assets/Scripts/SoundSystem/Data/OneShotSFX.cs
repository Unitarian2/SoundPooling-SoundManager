using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "OneShotSFX", menuName = "SoundSystem/OneShotSFX")]
public class OneShotSFX : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    public bool loop;
    public bool playOnAwake;
}
