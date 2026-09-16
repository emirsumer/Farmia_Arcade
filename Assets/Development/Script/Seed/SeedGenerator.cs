using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SeedArea 
{
    public Transform spawnSlot;
    public GameObject seedObject;
}
public class SeedGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> seedPrefab;
    [SerializeField] private List<SeedArea> seedAreaList;
    [SerializeField] private float spawnDuration;
    private void Start()
    {
        StartCoroutine(SpawnSeed());        
    }

    private void OnEnable()
    {
        FarmManager.Instance.OnSeedCollected += ListenOnSeedCollected;
    }

    private void OnDisable()
    {
        FarmManager.Instance.OnSeedCollected -= ListenOnSeedCollected;
    }
    private void ListenOnSeedCollected(Seed collectedSeed,SeedGenerator spawnedGenerator)
    {
        if (spawnedGenerator == this)
        {
            foreach (var s in seedAreaList)
            {
                if (s.seedObject == collectedSeed.gameObject)
                {
                    SeedArea a = new();
                    a.spawnSlot = s.spawnSlot;
                    a.seedObject = null;

                    int currentIndex = seedAreaList.IndexOf(s); // güncel s elemanýný currentýndexe atýyoruz
                    seedAreaList[currentIndex] = a;
                    break;
                }
            }
        }
    }
    private IEnumerator SpawnSeed()
    {
        yield return new WaitForSeconds(spawnDuration);
        foreach (var s in seedAreaList)
        {
            if (s.seedObject == null)
            {
                int randomIndex = UnityEngine.Random.Range(0, seedPrefab.Count);
                GameObject seed = Instantiate(seedPrefab[randomIndex], s.spawnSlot.position, Quaternion.identity);
                seed.GetComponent<Seed>().SpawnedGenerator = this;

                AudioManager.Instance.PlaySeedSpawn();

                SeedArea a = new();
                a.spawnSlot = s.spawnSlot;
                a.seedObject = seed;

                int currentIndex = seedAreaList.IndexOf(s);
                seedAreaList[currentIndex] = a;
                break;
            }
        }
        StartCoroutine(SpawnSeed());
    }
}
