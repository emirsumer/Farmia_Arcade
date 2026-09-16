using UnityEngine;

public class Vegetable : BaseItem
{
    private FarmArea _farmArea;

    private void OnEnable()
    {
        FarmManager.Instance.OnVegetableSpawned += ListenOnVegetableSpawned;
    }

    private void OnDisable()
    {
        FarmManager.Instance.OnVegetableSpawned -= ListenOnVegetableSpawned;
    }

    public override void OnInteract(CharacterController interactedCharacter)
    {
        base.OnInteract(interactedCharacter);
        FarmManager.Instance.OnVegetableCollected?.Invoke(this, _farmArea);
    }

    private void ListenOnVegetableSpawned(Vegetable v,FarmArea f)
    {
        if (v != this)
        {
            return;
        }
        _farmArea = f;
    }
}
