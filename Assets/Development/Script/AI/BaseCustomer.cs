using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerState
{
    Patrol,
    CheckProduct,
    Cash,
    Exit
}

public class BaseCustomer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private CustomerState customerState;
    [SerializeField] private Transform productTransform;
    [SerializeField] private Animator animator;

    private List<Transform> _patrolPoints = new();
    private List<Vegetable> _products = new();
    private void Start()
    {
        Transform patrolParent = GameObject.Find("PatrolPoints").transform;

        for (int i = 0; i < patrolParent.childCount; i++)
        {
            _patrolPoints.Add(patrolParent.GetChild(i));
        }

        StartCoroutine(PatrolState());
    }
    private void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
    private void DeclareState(CustomerState completedState)
    {
        switch (completedState)
        {
            case CustomerState.Patrol:

                int randomValue = Random.Range(0, 2);
                if (randomValue != 0)
                {
                    StartCoroutine(PatrolState());
                }
                else
                {
                    Shelf randomShelf = FarmManager.Instance.GetRandomShelfFromList();
                    if (randomShelf != null)
                    {
                        StartCoroutine(CheckProductState(randomShelf));
                    }
                    else
                    {
                        StartCoroutine(PatrolState());
                    }
                }

                break;

            case CustomerState.CheckProduct:

                Cash randomCash = FarmManager.Instance.GetRandomCashFromList();

                if (randomCash != null)
                {
                    StartCoroutine(CashState(randomCash));
                }
                else
                {
                    StartCoroutine(PatrolState());
                }

                break;

            case CustomerState.Cash:

                StartCoroutine(ExitState());

                break;

            case CustomerState.Exit:

                Destroy(gameObject);

                break;
        }
    }
    private IEnumerator PatrolState()
    {
        int randomIndex = Random.Range(0, _patrolPoints.Count);
        MoveAgentToDestination(_patrolPoints[randomIndex].position);

        while (CheckDistance() > 1f)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);
        DeclareState(CustomerState.Patrol);
    }
    private IEnumerator CheckProductState(Shelf targetShelf)
    {
        MoveAgentToDestination(targetShelf.CustomerTransform.position);

        while (CheckDistance() > 1f)
        {
            yield return new WaitForEndOfFrame();
        }

        int randomCount = Random.Range(1, 4);

        for (int i = 0; i < randomCount; i++)
        {
            Vegetable targetVegetable = targetShelf.GetVegetableFromList();

            if (targetVegetable != null)
            {
                targetShelf.RemoveVegetableFromList(targetVegetable);
                targetVegetable.transform.position = productTransform.position + (Vector3.up * i);
                targetVegetable.transform.parent = productTransform;

                _products.Add(targetVegetable);

                AudioManager.Instance.PlayAiTake();
            }
            else
            {
                if (i <= 0)
                {
                    StartCoroutine(PatrolState());
                }
                break; //raf boþaldýðý anda ürün alýnmýþ olsun olmasýn döngü hemen duruyor
            }
        }

        if (_products.Count > 0)
        {
            DeclareState(CustomerState.CheckProduct);
        }
    }
    private IEnumerator CashState(Cash targetCash)
    {
        MoveAgentToDestination(targetCash.CustomerTransform.position);

        while (CheckDistance() > 1f)
        {
            yield return new WaitForEndOfFrame();
        }

        for (int i = _products.Count - 1; i >= 0; i--)
        {
            targetCash.GenerateMoney(_products[i].Price);
            Destroy(_products[i].gameObject);
        }
        _products.Clear();

        AudioManager.Instance.PlayAiPay();

        DeclareState(CustomerState.Cash);
    }
    private IEnumerator ExitState()
    {
        MoveAgentToDestination(FarmManager.Instance.exitTransform.position);

        while (CheckDistance() > 1f)
        {
            yield return new WaitForEndOfFrame();
        }

        DeclareState(CustomerState.Exit);
    }
    private float CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, navMeshAgent.destination);
        return distance;
    }

    private void MoveAgentToDestination(Vector3 destination)
    {
        Vector3 targetDestination = destination;
        navMeshAgent.SetDestination(targetDestination);
    }
}