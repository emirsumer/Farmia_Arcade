using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    [SerializeField] private Transform cashTransform;
    public Transform CustomerTransform => cashTransform;

    [SerializeField] private List<Transform> moneyStackTransform;
    [SerializeField] private GameObject moneyPrefab;

    private int _pointIndex = 0;      
    private float _currentHeight = 0f;

    private List<GameObject> _moneyList = new List<GameObject>();

    private void Start()
    {
        FarmManager.Instance.AddCashToList(this);
    }

    public void GenerateMoney(float price)
    {
        if (moneyStackTransform.Count == 0) return;

        Vector3 spawnPosition = moneyStackTransform[_pointIndex].position;
        spawnPosition.y += _currentHeight;

        GameObject money = Instantiate(moneyPrefab, spawnPosition, Quaternion.identity);
        Money m = money.GetComponent<Money>();
        m.price = price;
        m.SetCash(this); // Paraya kasayý tanýtýr

        _moneyList.Add(money);
        _pointIndex++;

        //3 nokta da dolduysa en baþa dön ve üst kata çýk
        if (_pointIndex >= moneyStackTransform.Count)
        {
            _pointIndex = 0;
            _currentHeight += 0.25f;
        }
    }

    public void MoneyRemoved(GameObject moneyObject)
    {
        _moneyList.Remove(moneyObject);

        if (_moneyList.Count == 0)
        {
            _pointIndex = 0;
            _currentHeight = 0f;
        }
    }
}
