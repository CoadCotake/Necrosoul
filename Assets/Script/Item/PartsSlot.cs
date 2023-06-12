using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsSlot : MonoBehaviour
{   //∆ƒ√˜ΩΩ∑‘¿∫ 100∫Œ≈Õ Ω√¿€
    [SerializeField] int SlotNumber;
    public Item ItemData;
    public bool SlotCheck;

    private void Start()
    {
        SlotCheck = true;
    }
    public int Gain(Sprite sprite)
    {
        this.gameObject.GetComponent<Image>().sprite = sprite;
        SlotCheck = false;
        return SlotNumber;
    }
    public int ReturnNumber()
    {
        return SlotNumber;
    }
    public void SetNumber(int Set)
    {
        SlotNumber = Set;
    }
}
