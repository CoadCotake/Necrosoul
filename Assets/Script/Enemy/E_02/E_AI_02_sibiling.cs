using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_02_sibiling : MonoBehaviour
{
    public Transform Player_Tp;
    float speed;
    float speed_weight;
    public float chase_route_change_time;
    float chase_route_change_timer;
    public Vector3 chase_dir;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Player_Tp = GameObject.FindGameObjectWithTag("Player").transform;
        if (Player_Tp != null)
        {
            chase_route_change_timer -= Time.deltaTime;
            if (chase_route_change_timer <= 0)
            {
                chase_dir = Player_Tp.position - this.transform.position;
                chase_route_change_timer = chase_route_change_time;
                speed_weight = Random.Range(0, 1f);
            }
            transform.Translate(chase_dir.normalized * (speed + speed_weight) * Time.deltaTime);
        }
        else
        {
            Destroy(this.transform.parent.gameObject);
        }
        Debug.DrawRay(this.transform.position, chase_dir,Color.red);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
