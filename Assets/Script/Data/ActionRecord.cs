using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ActionRecord  //매 스테이지 기록
{
    public int EnemyKill;
    public int Money;
    public float Time;
    public float Damge;
    public float Hit;
    public int Died;
    public int ClearStage;

    public void Reset()
    {
        EnemyKill = 0;
        Money = 0;
        Time = 0;
        Damge = 0;
        Hit = 0;
        Died = 0;
        ClearStage = 0;
    }
}
