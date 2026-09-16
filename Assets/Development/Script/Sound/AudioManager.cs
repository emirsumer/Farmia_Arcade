using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;

    [SerializeField] private AudioSource seedSpawnSfx;
    [SerializeField] private AudioSource seedPlantSfx;
    [SerializeField] private AudioSource seedCollectSfx;

    [SerializeField] private AudioSource vegetableReadySfx;
    [SerializeField] private AudioSource vegetableCollectSfx;
    [SerializeField] private AudioSource shelfPlaceSfx;

    [SerializeField] private AudioSource aiTakeSfx;
    [SerializeField] private AudioSource aiPaySfx;

    [SerializeField] private AudioSource rightStepSfx;
    [SerializeField] private AudioSource leftStepSfx;

    [SerializeField] private AudioSource moneySfx;
    [SerializeField] private AudioSource clickSfx;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMenuMusic()
    {
        if (gameMusic.isPlaying)
        {
            gameMusic.Stop();
        }
        menuMusic.Play();
    }

    public void PlayGameMusic()
    {
        if (menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }
        gameMusic.Play();
    }

    public void PlaySeedSpawn()
    {
        if (seedSpawnSfx != null)
        {
            seedSpawnSfx.PlayOneShot(seedSpawnSfx.clip);
        }
    }
    public void PlaySeedPlant()
    {
        {
            if (seedPlantSfx != null)
                seedPlantSfx.PlayOneShot(seedPlantSfx.clip);
        }
    }
    public void PlaySeedCollect()
    {
        if (seedCollectSfx != null)
        {
            seedCollectSfx.PlayOneShot(seedCollectSfx.clip);
        }
    }
    public void PlayVegetableReady()
    {
        if (vegetableReadySfx != null)
        {
            vegetableReadySfx.PlayOneShot(vegetableReadySfx.clip);
        }
    }
    public void PlayVegetableCollect()
    {
        if (vegetableCollectSfx != null)
        {
            vegetableCollectSfx.PlayOneShot(vegetableCollectSfx.clip);
        }
    }
    public void PlayShelfPlace()
    {
        if (shelfPlaceSfx != null)
        {
            shelfPlaceSfx.PlayOneShot(shelfPlaceSfx.clip);
        }
    }

    public void PlayAiTake()
    {
        if (aiTakeSfx != null)
        {
            aiTakeSfx.PlayOneShot(aiTakeSfx.clip);
        }
    }
    public void PlayAiPay()
    {
        if (aiPaySfx != null)
        {
            aiPaySfx.PlayOneShot(aiPaySfx.clip);
        }
    }

    public void PlayRightStepSfx()
    {
        if (rightStepSfx != null)
        {
            rightStepSfx.Play();
        }
    }
    public void PlayLeftStepSfx()
    {
        if (leftStepSfx != null)
        {
            leftStepSfx.Play();
        }

    }

    public void PlayMoneySfx()
    {
        if (moneySfx != null)
        {
            moneySfx.PlayOneShot(moneySfx.clip);
        }
    }
    public void PlayClickSfx()
    {
        if (clickSfx != null)
        {
            clickSfx.PlayOneShot(clickSfx.clip);
        }
    }
    public void PauseGameMusic()
    {
        if (gameMusic.isPlaying)
        {
            gameMusic.Pause();
        }
    }

    public void ResumeGameMusic()
    {
        gameMusic.Play();
    }
}
