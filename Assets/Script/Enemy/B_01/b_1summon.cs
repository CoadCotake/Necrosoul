using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b_1summon : MonoBehaviour
{
  

    Rigidbody2D rgd;
    public List<node> path;
    Vector2 dir;
    void Start()
    {
        
        rgd = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count != 0)
        {
            dir = path[1].pos - (Vector2)this.transform.position;
                rgd.AddForce(dir.normalized * 5);
            enemy_camera_fitting();
        }
        //transform.Translate(dir.normalized * 5* Time.deltaTime);
    }
    void enemy_camera_fitting()
    {
        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        /*if (viewport_pos.x < -0.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);
        if (viewport_pos.y < -0.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);
        if (viewport_pos.x > 1.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse); 
        if (viewport_pos.x > 1.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);*/


        if (transform.rotation.y > 360)
            transform.Rotate(0, -360, 0);
    }
}
