using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundSource : MonoBehaviour
{
    AudioSource audioSource;
    Coroutine playingCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource) audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void Initialize(OneShotSFX oneShotSFX)
    {
        audioSource.clip = oneShotSFX.clip;
        audioSource.outputAudioMixerGroup = oneShotSFX.mixerGroup;
        audioSource.loop = oneShotSFX.loop;
        audioSource.playOnAwake = oneShotSFX.playOnAwake;
    }

    public void Play()
    {
        if(playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }

        audioSource.Play();
        playingCoroutine = StartCoroutine(WaitForSoundToEnd());
    }

    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        SoundManager.Instance.ReturnToPool(this);
    }

    public void Stop()
    {
        if(playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }

        audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }
}
