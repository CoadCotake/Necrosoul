using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_effect_rotation : MonoBehaviour
{
    ParticleSystem ps;
    public Material dash_ghost;
    public Material dash_ghost_mirror;
    // Start is called before the first frame update
    float dash_ghost_effect_timer;
    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        ParticleSystemRenderer r = this.GetComponent<ParticleSystemRenderer>();
        if (Gamemanager.GM.Player_obj.GetComponent<PlayerCharacter>().direction == 1)
            r.material = dash_ghost;
        else
            r.material = dash_ghost_mirror;
        dash_ghost_effect_timer = ps.main.duration + 0.02f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dash_ghost_effect_timer > 0)
        {
            dash_ghost_effect_timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        //ParticleSystem.MainModule m=ps.main;

        //m.startRotation =-1* Gamemanager.GM.Player_obj.transform.eulerAngles.z*Mathf.Deg2Rad;
    }
    public void OnEnable()
    {
        ps = this.GetComponent<ParticleSystem>();
        ParticleSystemRenderer r = this.GetComponent<ParticleSystemRenderer>();
        if (Gamemanager.GM.Player_obj.GetComponent<PlayerCharacter>().direction == 1)
            r.material = dash_ghost;
        else
            r.material = dash_ghost_mirror;

        dash_ghost_effect_timer = ps.main.duration + 0.02f;
    }
}
