using UnityEngine;

public class Money : MonoBehaviour, IInteraction
{
    public float price;
    private Cash _cash;

    public void SetCash(Cash cash)
    {
        _cash = cash;
    }

    public void OnInteract(CharacterController interactedCharacter)
    {
        GameManager.Instance.UpdateCoin(price);

        if (_cash != null)
        {
            _cash.MoneyRemoved(gameObject);
        }

        Destroy(gameObject);
        AudioManager.Instance.PlayMoneySfx();
    }
}