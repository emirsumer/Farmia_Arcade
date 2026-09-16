using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float totalCoin;

    public Action<float> OnCoinUpdated;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayGameMusic();
        OnCoinUpdated?.Invoke(totalCoin);
    }

    public void UpdateCoin(float amount)
    {
        totalCoin += amount;
        OnCoinUpdated?.Invoke(totalCoin);
    }

    public bool IsCoinEnough(float amount)
    {
        return totalCoin >= amount;
    }
}
