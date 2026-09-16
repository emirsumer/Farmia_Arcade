using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShelfSlot
{
    public GameObject slotObject;
    private Vegetable _item;

    public void SetItem(Vegetable newItem)
    {
        _item = newItem;
    }
    public Vegetable GetItem()
    {
        return _item;
    }
}

public class Shelf : MonoBehaviour, IInteraction
{
    [SerializeField] private List<ShelfSlot> shelfSlots;
    [SerializeField] private Transform customerTransform;
    public Transform CustomerTransform => customerTransform;
   
    private void Start()
    {
        FarmManager.Instance.AddShelfToList(this);
    }
    public void OnInteract(CharacterController interactedCharacter)
    {
       for (int i = 0; i < shelfSlots.Count; i++)
        {
            if (shelfSlots[i].GetItem() == null)
            {
                Vegetable v = interactedCharacter.GetVegetableFromList();

                if (v != null)
                {
                    ShelfSlot s = new();
                    s.slotObject = shelfSlots[i].slotObject;
                    s.SetItem(v);
                    v.CanInteractable = false;
                    v.transform.position = s.slotObject.transform.position;
                    v.transform.parent = s.slotObject.transform;
                    interactedCharacter.RemoveItemFromList(v);

                    shelfSlots[i] = s;
                    AudioManager.Instance.PlayShelfPlace();
                    break;
                }
            }
        }
    }

    public Vegetable GetVegetableFromList()
    {
        int lastIndex = -1;
        foreach (var s in shelfSlots)
        {
            if (s.GetItem() != null)
            {
                lastIndex++;
            }
        }

        if (lastIndex > -1)
        {
            return shelfSlots[lastIndex].GetItem();
        }
        else
        {
            return null;
        }
    }

    public void RemoveVegetableFromList(Vegetable targetVegetable)
    {
        foreach (var s in shelfSlots)
        {
            if (s.GetItem() == targetVegetable)
            {
                ShelfSlot newSlot = new();
                newSlot.slotObject = s.slotObject;
                newSlot.SetItem(null);

                shelfSlots.Remove(s); //günceli çýkar
                shelfSlots.Add(newSlot); //yeniyi ekle
                break;
            }
        }
    }
}
