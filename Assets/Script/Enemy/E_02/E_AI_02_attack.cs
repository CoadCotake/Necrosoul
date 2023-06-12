using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_02_attack : MonoBehaviour
{
     public GameObject sibiling;
    Rigidbody2D rgd2D;
    public Vector2 dir;
    public float bullet_spped;

    void Start()
    {
        rgd2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rgd2D.velocity = (dir.normalized * bullet_spped);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            GameObject sib = Instantiate(sibiling, transform.position, Quaternion.identity);
            // var E_AI = creator.GetComponent<E_AI_01>();
            //E_AI.created_object.Remove(this.gameObject);
        }
    }
}
