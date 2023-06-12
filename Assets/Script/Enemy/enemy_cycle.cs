using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cycle : MonoBehaviour
{
    public int Level;
    public List<GameObject> enemy_group = new List<GameObject>();
    public GameObject choose_group;
    // Start is called before the first frame update
    void Start()
    {
       /* for (int i = 0; i < enemy_group.Count; i++)
        {
            enemy_group[i].SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (choose_group != null)
        {
            if (!choose_group.activeSelf)
                choose_group.SetActive(true);
        }
        else
        {
            this.transform.parent.parent.GetComponent<room>().tim = 1.2f;
        }
        if (enemy_group.Count == 0)
        {
            this.gameObject.transform.parent.GetComponent<normal_contents>().room_cleared = true;
            Destroy(this.gameObject);
        }
    }
    public void acitve_enemy()
    {

       
            enemy_group[0].gameObject.SetActive(true);
        choose_group= enemy_group[0].gameObject;
        



    }
    public void delete_enemy(GameObject a)
    {
        enemy_group.Remove(a);
    }
}
