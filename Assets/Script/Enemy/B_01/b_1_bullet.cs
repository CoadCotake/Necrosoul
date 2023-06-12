using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b_1_bullet : MonoBehaviour
{
   public Vector3 Dir;
    public float b_speed;
    Rigidbody2D rgd;
    // Start is called before the first frame update
    void Start()
    {
        rgd = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rgd.AddForce(Dir * b_speed * Time.deltaTime);
    }
}
