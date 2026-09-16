using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform itemPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    private Rigidbody _rigidbody;
    private List<BaseItem> _itemList = new();
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        PlayerInput.Instance.OnPlayerMove += MoveCharacter;
        FarmManager.Instance.OnSeedPlanted += ListenOnSeedPlanted;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.OnPlayerMove -= MoveCharacter;
        FarmManager.Instance.OnSeedPlanted -= ListenOnSeedPlanted;
    }
    private void MoveCharacter(Vector2 moveDirection)
    {
        Vector3 calculatesPosition = new Vector3(moveDirection.x * moveSpeed, _rigidbody.linearVelocity.y,moveDirection.y * moveSpeed);
        _rigidbody.linearVelocity = calculatesPosition;
        
        if(moveDirection.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(calculatesPosition);
            lookRotation.x = 0; 
            lookRotation.z = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 250 * Time.deltaTime);
        }
        animator.SetFloat("Move", moveDirection.magnitude);
    }

    public void AttachItem(BaseItem item)
    {
        Vector3 desiredPosition = new Vector3(itemPosition.position.x, itemPosition.position.y + _itemList.Count, itemPosition.position.z);
        item.transform.position = desiredPosition;
        item.transform.parent = transform;
        _itemList.Add(item);
    }

    public void RefreshItemPositions()
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            Vector3 iPosition = new Vector3(itemPosition.position.x, itemPosition.position.y + i , itemPosition.position.z);
            _itemList[i].transform.position = iPosition;
        }

        //foreach (BaseItem item in _itemList)
        //{
        //    Vector3 iPosition = new Vector3(itemPosition.position.x, itemPosition.position.y + _itemList.IndexOf(item), itemPosition.position.z);
        //    item.transform.position = iPosition;
        //}
    }

    public List<BaseItem> GetItemList()
    {
        return _itemList;
    }
    public Seed GetSeedFromList()
    {
        Seed result = null;
        foreach(BaseItem item in _itemList)
        {
            if(item is Seed)
            {
                result = item as Seed;
                break;
            }
        }
        return result;
    }
    public Vegetable GetVegetableFromList()
    {
        Vegetable result = null;
        foreach (BaseItem item in _itemList)
        {
            if (item is Vegetable)
            {
                result = item as Vegetable;
                break;
            }
        }
        return result;
    }
    public void RemoveItemFromList(BaseItem item)
    {
        _itemList.Remove(item);
        RefreshItemPositions();
    }
    private void ListenOnSeedPlanted(Seed seedObject,FarmArea farmArea)
    {
        RemoveItemFromList(seedObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteraction interaction = other.GetComponent<IInteraction>();
        if(interaction != null)
        {
            interaction.OnInteract(this);
        }
    }
}
