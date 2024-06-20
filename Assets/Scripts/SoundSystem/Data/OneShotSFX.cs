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
    public bool frequentSound;

    [SerializeField]
    [MinMaxRange(0, 1)]
    RangedFloat volume = new RangedFloat(.8f, .8f);

    [SerializeField]
    [MinMaxRange(0, 2)]
    RangedFloat pitch = new RangedFloat(.95f, 1.05f);

    public RangedFloat Volume => volume;
    public RangedFloat Pitch => pitch;
}
