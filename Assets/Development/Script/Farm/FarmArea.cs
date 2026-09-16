using System.Collections;
using UnityEngine;

public class FarmArea : MonoBehaviour, IInteraction
{
    [SerializeField] private Transform spawnTransform;
    public Transform SpawnTransform { get => spawnTransform; }

    private BaseItem _item;
    private Seed _targetSeed;
    private void OnEnable()
    {
        FarmManager.Instance.OnVegetableSpawned += ListenOnVegetableSpawned;
        FarmManager.Instance.OnVegetableCollected += ListenOnVegetableCollected;
    }
    private void OnDisable()
    {
        FarmManager.Instance.OnVegetableSpawned -= ListenOnVegetableSpawned;
        FarmManager.Instance.OnVegetableCollected -= ListenOnVegetableCollected;

    }
    public void OnInteract(CharacterController interactedCharacter)
    {
        if (interactedCharacter.GetItemList().Count > 0)
        {
            if (_item == null)
            {
                _targetSeed = interactedCharacter.GetSeedFromList();

                if (_targetSeed != null)
                {
                    UseTheSeed(_targetSeed);
                   // interactedCharacter.RemoveItemFromList(_targetSeed);
                   // StartCoroutine(_targetSeed.SpawnVegetable(this));
                }
            }
        }
    }
    private void UseTheSeed(Seed seedObject)
    {
        seedObject.transform.position = transform.position;
        seedObject.transform.parent = transform;

        _item = seedObject;

        Collider seedCollider = seedObject.GetComponent<Collider>();
        Destroy(seedCollider);

        AudioManager.Instance.PlaySeedPlant();

        FarmManager.Instance.OnSeedPlanted?.Invoke(seedObject,this);
    }
    private void ListenOnVegetableSpawned(Vegetable v , FarmArea f)
    {
        if (f != this)
        {
            return;
        }
        _item = v;
    }
    private void ListenOnVegetableCollected(Vegetable v, FarmArea f)
    {
        if (f != this)
        {
            return;
        } 
        _item = null;
    }
}
