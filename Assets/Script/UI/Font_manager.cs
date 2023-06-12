using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class Font_manager : MonoBehaviour
{
    public static Font_manager DN;
    [SerializeField]
    List<DamageNumber> Prefabs;
    // Start is called before the first frame update
    void Awake()
    {
        DN = this;
        Prefabs = new List<DamageNumber>();
        Transform number = transform.Find("Number");
        for (int i = 0; i < number.childCount; i++)
        {
            Prefabs.Add(number.GetChild(i).GetComponent<DamageNumber>());
        }
        number.gameObject.SetActive(false);

    }

    public void SpawnNumber(int index, int number, Transform tr)
    {
        Vector3 position = tr.position;
        position.z = 0;
        Prefabs[index].Spawn(position, number);
    }

    public void SpawnText(int index, string text, Transform tr)
    {
        Vector3 position = tr.position;
        position.z = 0;
        Prefabs[index].Spawn(position, text);
    }
}
