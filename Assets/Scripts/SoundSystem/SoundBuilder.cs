using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundBuilder
{
    private readonly SoundManager soundManager;
    private OneShotSFX oneShotSFX;
    Vector3 position = Vector3.zero;
    float pitchValue = 1f;
    float volumeValue = 1f;

    public SoundBuilder(SoundManager soundManager)
    {
        this.soundManager = soundManager;  
    }

    public SoundBuilder SetPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomPitch(float minValue = 0.95f, float maxValue = 1.05f)
    {
        pitchValue = 1f;
        pitchValue = Random.Range(minValue, maxValue);
        return this;
    }

    public SoundBuilder WithRandomVolume(float minValue = 0.9f, float maxValue = 1f)
    {
        volumeValue = 1f;
        volumeValue = Random.Range(minValue, maxValue);
        return this;
    }

    public void Play(OneShotSFX oneShotSFX)
    {
        if (oneShotSFX == null)
        {
            Debug.LogError("SoundData is null");
            return;
        }

        if (!soundManager.CanPlaySound(oneShotSFX)) return;

        SoundSource soundSource = soundManager.Get();
        soundSource.Initialize(oneShotSFX);
        soundSource.transform.position = position;
        soundSource.transform.parent = SoundManager.Instance.transform;

        soundSource.SetVariationValues(pitchValue, volumeValue);

        if (oneShotSFX.frequentSound)
        {
            soundSource.Node = soundManager.frequentSoundSources.AddLast(soundSource);
        }

        soundSource.Play();
    }

}
