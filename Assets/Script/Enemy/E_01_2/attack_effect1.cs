using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_effect1 : MonoBehaviour
{
    Vector3 a;
    Rigidbody2D rgd2D;
    public Vector2 dir;
    public GameObject creator;
    public int Attack;
    public float bullet_spped;
    public Vector2 hitted_force;
    Vector2 p_pos;
    // Start is called before the first frame update
    void Start()
    {
        rgd2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rgd2D.velocity=(dir.normalized * bullet_spped);
    }
    private void OnDisable()
    {
        transform.position = new Vector2(-999, -999);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            this.gameObject.SetActive(false);
            // var E_AI = creator.GetComponent<E_AI_01>();
            //E_AI.created_object.Remove(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter p = collision.GetComponent<PlayerCharacter>();

            p.hitted_event(this.transform.position);
            p.player_hitted(Attack);
            this.gameObject.SetActive(false);
        }
    }
}
