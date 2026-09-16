using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
   public static FarmManager Instance;

    public Action<Seed,SeedGenerator> OnSeedCollected;
    public Action<Seed, FarmArea> OnSeedPlanted; //FarmArea UseTheSeed Invoke ediyoruz - karakter seedi dinliyor itemi remove için ve seed dinliyor vegetable üretmesi için
    public Action<Vegetable,FarmArea> OnVegetableSpawned; //Seed coroutine bitince invoke ediyoruz - FarmArea ve Vegetable  kendini setlemek için
    public Action<Vegetable, FarmArea> OnVegetableCollected; //Vegetable OnInteract oldugunda Invoke ediyoruz - Farmarea bunu null yapmak için dinliyor

    public Transform exitTransform;

    private List<Shelf> _allShelves = new();
    private List<Cash> _allCashes = new();

    public int GetShelfCount() => _allShelves.Count;
    public int GetCashCount() => _allCashes.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }



    public void AddShelfToList(Shelf targetShelf)
    {
        _allShelves.Add(targetShelf);
    }

    public Shelf GetRandomShelfFromList()
    {
        int randomIndex = UnityEngine.Random.Range(0, _allShelves.Count);
        if (_allShelves.Count > 0)
        {
            return _allShelves[randomIndex];
        }
        else
        {
            return null;
        }
    }

    public void AddCashToList(Cash targetCash)
    {
        _allCashes.Add(targetCash);
    }

    public Cash GetRandomCashFromList()
    {
        int randomIndex = UnityEngine.Random.Range(0, _allCashes.Count);
        if (_allCashes.Count > 0)
        {
            return _allCashes[randomIndex];
        }
        else
        {
            return null;
        }
    }

}
