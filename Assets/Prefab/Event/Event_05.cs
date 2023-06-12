using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_05 : MonoBehaviour
{
    public void lose_health()
    {
        Player_status.p_status.lose_hp(10);
    }
    public void lose_money()
    {
        Gamemanager.GM.game_ev.when_lose_money(10);
    }
}
