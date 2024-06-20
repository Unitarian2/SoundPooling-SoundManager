using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundSource : MonoBehaviour
{
    public OneShotSFX Data {  get; private set; }
    AudioSource audioSource;
    Coroutine playingCoroutine;
    public LinkedListNode<SoundSource> Node { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource) audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void Initialize(OneShotSFX oneShotSFX)
    {
        Data = oneShotSFX;
        audioSource.clip = oneShotSFX.clip;
        audioSource.outputAudioMixerGroup = oneShotSFX.mixerGroup;
        audioSource.loop = oneShotSFX.loop;
        audioSource.playOnAwake = oneShotSFX.playOnAwake;
    }

    public void Play()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }

        audioSource.Play();
        playingCoroutine = StartCoroutine(WaitForSoundToEnd());
    }

    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Stop();
    }

    public void Stop()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }

        audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }

    public void SetVariationValues(float pitchValue, float volumeValue)
    {
        
        audioSource.pitch = pitchValue;
        audioSource.volume = volumeValue;
    }


    
}
