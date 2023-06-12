using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_tsst : MonoBehaviour
{
    public Transform player;
    public Vector3 dir;
    public Vector3 point;
    public Transform eye;
    public GameObject test;
    public Vector3 move_speed;
    Rigidbody2D rgd;
    bool weight_dir;
    int random;
    float timer;
    private Vector2 Vector;

    void Start()
    {
        rgd = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        move_speed =new Vector3(7,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        /*timer += Time.deltaTime;//랜덤이동
        if (timer >= 1)
        {
            random = Random.Range(0, 3);
            Debug.Log(random);
            timer = 0;
        }
        switch (random)
        {
            case 0:
                break;
            case 1:
                transform.Translate(-1 * move_speed * Time.deltaTime);
                break;
            case 2:
                transform.Translate(move_speed * Time.deltaTime);
                break;
        }*/
        dir = this.transform.position - player.transform.position;
       
        if (this.transform.position.x > player.transform.position.x + 4)
        {
            weight_dir = true;
        }
        else if(this.transform.position.x < player.transform.position.x - 4)
        {
            weight_dir = false;
        }

        if (weight_dir)
        {
            transform.Translate(-1 * move_speed * Time.deltaTime);
        }
        else
        {
            transform.Translate( move_speed * Time.deltaTime);
        }
        
        
        /*dir =  player.position- this.transform.position;
        LayerMask l = 12;
        Debug.DrawLine(transform.position, player.position, Color.red);
        Debug.DrawLine(transform.position,-1*dir*100, Color.blue);
        RaycastHit2D r = Physics2D.Raycast(    transform.position, dir, 10.0f,LayerMask.GetMask("platform_can't_passs"));
       if(r.collider!=null)
            Debug.Log(r.transform.gameObject.name);//레이캐스트 쏴서 못통과하는 벽 찿기
        */
        /*if (r.transform.gameObject.layer == 12)
        {
            Debug.Log("1");
            point += Vector3.up * Time.deltaTime;
        }
        else
        {
            Debug.Log("2");
            point = Vector3.zero;
        }*/

        /* if (Physics2D.Raycast(dir, transform.position, 30.0f,12))
         {
             Debug.Log("1");
             point += Vector3.up * Time.deltaTime;
         }
         else
         {
             Debug.Log("2");
             point = Vector3.zero;
         }*/
        transform.Translate((dir.normalized + point) * 3 * Time.deltaTime);

    }
}
