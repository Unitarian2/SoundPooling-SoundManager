using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : Singleton<SoundManager>
{
    private IObjectPool<SoundSource> soundSourcePool;
    private readonly List<SoundSource> activeSoundSources = new();
    public readonly Dictionary<OneShotSFX, int> oneShotCounts = new();

    [Header("Prefab")]
    [SerializeField] private SoundSource soundSourcePrefab;

    [Header("Sound Pool Settings")]
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private int maxSoundInstances = 30;

    private void Start()
    {
        InitalizePool();
    }

    private void InitalizePool()
    {
        soundSourcePool = new ObjectPool<SoundSource>(
                CreateSoundSource,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPool,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
    }

    private SoundSource CreateSoundSource()
    {
        var soundSource = Instantiate(soundSourcePrefab);
        soundSource.gameObject.SetActive(false);
        return soundSource;
    }

    private void OnTakeFromPool(SoundSource soundSource)
    {
        soundSource.gameObject.SetActive(true);
        activeSoundSources.Add(soundSource);
    }

    private void OnReturnedToPool(SoundSource soundSource)
    {
        soundSource.gameObject.SetActive(false);
        activeSoundSources.Remove(soundSource);
    }

    private void OnDestroyPool(SoundSource soundSource)
    {
        Destroy(soundSource.gameObject);
    }

    public SoundSource Get()
    {
        return soundSourcePool.Get();
    }

    public void ReturnToPool(SoundSource soundSource)
    {
        soundSourcePool.Release(soundSource);
    }

    public bool CanPlaySound(OneShotSFX oneShotSFX)
    {
        if(oneShotCounts.TryGetValue(oneShotSFX, out var count))
        {
            if(count >= maxSoundInstances)
            {
                return false;
            }
        }

        return true;
    }
}
