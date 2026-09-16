using UnityEngine;

public abstract class BaseItem : MonoBehaviour, IInteraction
{
    [SerializeField]  protected float price;
    public float Price
    {
        get { return price; }
    }
    private bool _canInteractable = true;

    public bool CanInteractable
    {
        get { return _canInteractable; }
        set { _canInteractable = value; }
    }

    public virtual void OnInteract(CharacterController interactedCharacter)
    {
        if (!_canInteractable)
        {
            return;
        }

        interactedCharacter.AttachItem(this);

        if (this is Seed)
        {
            AudioManager.Instance.PlaySeedCollect();
        }
        else
        {
            AudioManager.Instance.PlayVegetableCollect();

        }
    }
}
