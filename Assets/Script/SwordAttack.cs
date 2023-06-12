using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Transform Pos;
    public Vector2 boxSize;
    float attack_timer;
    public float attack_time;
    public bool attack_animating;
    /// ///////////////////
    bool first_anim;
    public int attack_anim_frame;
    Player_animator p_anim;
    // Start is called before the first frame update
    void Start()
    {
        p_anim = this.transform.parent.GetComponent<Player_animator>();
    }
    public void swordAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(attack_timer<=0)
                attack_timer = attack_time;
        }
        if (attack_timer > 0)
        {
            attack_timer -= Time.deltaTime;
        }
        if (!attack_animating && attack_timer > 0)
        {
            attack_timer = 0;
           
                    p_anim.sword_attack_anim();

      
        }
     //플레이어 공격 클립에 다음의 이벤트를 작용 1.클립 시작할때 attack_animating을 true로 하고 끝나기 직적에 false(3번 공격은 후딜을 길게 주기위해 완전히 끝난 후)2.true로하는 프레임에 attack_anim_frame+1 2일때는 0 <-anim_frame변수는 에니메이터에 선언 후 변경 가능 3.이펙트 생성 이펙트는 bullet과 유사한 데이터를 가ㅣㄴ다

       
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D[] Hits = Physics2D.OverlapBoxAll(Pos.position, boxSize, 0);
            foreach (Collider2D collider in Hits)
            {
                if(collider.tag=="Enemy")
                {
                    Debug.Log("근접공격!");     //공격 이벤트 및 계산 넣으면 됨
                }
            }
        }
    }
    private void FixedUpdate()
    {
        swordAttack();
    }

    private void OnDrawGizmos()     //임시로 볼수있게하는 함수
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize);
    }
}
