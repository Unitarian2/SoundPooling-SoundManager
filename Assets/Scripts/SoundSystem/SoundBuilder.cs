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

    public SoundBuilder(SoundManager soundManager)
    {
        this.soundManager = soundManager;  
    }

    public SoundBuilder SetOneShotSFX(OneShotSFX oneShotSFX)
    {
        this.oneShotSFX = oneShotSFX;
        return this;
    }

    public SoundBuilder SetPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomPitch(float minValue = -0.05f, float maxValue = 0.05f)
    {
        pitchValue = 1f;
        pitchValue += Random.Range(minValue, maxValue);
        return this;
    }

    public void Play()
    {
        if (!soundManager.CanPlaySound(oneShotSFX)) return;

        SoundSource soundSource = soundManager.Get();
        soundSource.Initialize(oneShotSFX);
        soundSource.transform.position = position;
        soundSource.transform.parent = SoundManager.Instance.transform;

        soundSource.SetPitchValue(pitchValue);

        if (oneShotSFX.frequentSound)
        {
            soundSource.Node = soundManager.frequentSoundSources.AddLast(soundSource);
        }

        soundSource.Play();
    }

}
