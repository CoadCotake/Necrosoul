using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_attack_effect : MonoBehaviour
{
    public GameObject bullet;
   public bool OutCheck;   //삭제할건지 체크
    public int b_count;
    public int Damge;     //데미지
    public float Speed;     //총알스피드
    public Transform[] b_pos;
    private void Awake()
    {
        GameObject a= Instantiate(bullet, b_pos[0].position, this.transform.rotation);
        a.transform.SetParent(b_pos[0]);
        GameObject b = Instantiate(bullet, b_pos[1].position, this.transform.rotation);
        b.transform.SetParent(b_pos[1]);
    }
    private void Update()
    {
        if (b_count == 2)
        {
            Bullet b = this.GetComponent<Bullet>();
            b.DestroyBullet();
        }
    }
}
//////////////// 대책 1 풀링할 때 오브젝트 대책 2 총알 파괴될때 찿아서