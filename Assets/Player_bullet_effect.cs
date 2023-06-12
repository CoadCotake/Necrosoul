using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet_effect : MonoBehaviour
{
    float min_length_range = 10;
    float min_length;
    int num=-1;
    public float speed = 1;
    float length;
    GameObject homing_target;
    void Update()
    {
        min_length = min_length_range;
    }
    void homing_effect()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         //length= new float[enemies.Length];
       
       
        for(int i = 0; i < enemies.Length; i++)
        {
            length=Vector3.Distance(enemies[i].transform.position,transform.position);
            if (length < min_length) {
                min_length = length;
                num = i;
                    }
        }
        if (num != -1)
        {
            homing_target = enemies[num];
        }
        if (homing_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, homing_target.transform.position, Time.deltaTime * speed);
        }
    }
}
/*호밍 어떤 방식을 원하는가 물어보기
 * 가시 쳐내*/
