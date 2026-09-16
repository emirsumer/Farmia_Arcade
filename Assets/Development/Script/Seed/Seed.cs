using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Seed : BaseItem
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject vegetablePrefab;

    private SeedGenerator _spawnedGenerator;
    public SeedGenerator SpawnedGenerator
    {
        get => _spawnedGenerator; set => _spawnedGenerator = value;
    }

    private void OnEnable()
    {
        FarmManager.Instance.OnSeedPlanted += ListenOnSeedPlanted;
    }

    private void OnDisable()
    {
        FarmManager.Instance.OnSeedPlanted -= ListenOnSeedPlanted;

    }

    private void ListenOnSeedPlanted(Seed seedObject,FarmArea farmArea)
    {
        if (seedObject != this)
        {
            return;
        }
       
        StartCoroutine(SpawnVegetable(farmArea));
    }
    public override void OnInteract(CharacterController interactedCharacter)
    {
        bool isEnoughCoin = GameManager.Instance.IsCoinEnough(Mathf.Abs(price));

        if (isEnoughCoin)
        {
            base.OnInteract(interactedCharacter);
            GameManager.Instance.UpdateCoin(price);
            FarmManager.Instance.OnSeedCollected?.Invoke(this, _spawnedGenerator);
        }

    }
    private IEnumerator SpawnVegetable(FarmArea farmArea)
    {
        yield return new WaitForSeconds(duration);
        GameObject spawnedVegetable = Instantiate(vegetablePrefab, farmArea.SpawnTransform.position, Quaternion.identity);
        Vegetable v = spawnedVegetable.GetComponent<Vegetable>();
        AudioManager.Instance.PlayVegetableReady();
        FarmManager.Instance.OnVegetableSpawned?.Invoke(v,farmArea);

        Destroy(gameObject);
    }
}
