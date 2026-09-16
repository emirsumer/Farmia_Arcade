using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> customerPrefabs = new();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform patrolParent;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private int maxCustomerCount = 10;

    private List<Transform> _patrolPoints = new();
    private List<BaseCustomer> _activeCustomers = new();

    private void Start()
    {
        for (int i = 0; i < patrolParent.childCount; i++)
        {
            _patrolPoints.Add(patrolParent.GetChild(i));
        }

        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            _activeCustomers.RemoveAll(customer => customer == null);

            if (_activeCustomers.Count < maxCustomerCount && customerPrefabs.Count > 0)
            {
                SpawnCustomer();
            }
        }
    }
    private void SpawnCustomer()
    {
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        GameObject selectedPrefab = customerPrefabs[randomIndex];

        GameObject customerObject = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);

        BaseCustomer customer = customerObject.GetComponent<BaseCustomer>();

        _activeCustomers.Add(customer);
    }
}

