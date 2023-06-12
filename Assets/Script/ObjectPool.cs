using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;
    Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();
    
    private void Awake()
    {
        Instance = this;
        Initialize(30);
    }
    private void Initialize(int initCount)  //미리생성하기
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Bullet CreateNewObject()        //총알 생성
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false); 
        newObj.transform.SetParent(this.transform);
        return newObj;
    }
    public static Bullet GetObject(Transform Gun,Transform Shoot,int dmg, float spd,Vector2 direction)    //오브젝트 받아오기
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.transform.position = Shoot.position;
            obj.transform.rotation = Gun.rotation;
            obj.gameObject.SetActive(true);
            obj.gameObject.GetComponent<Bullet>().Damge = dmg;
            obj.gameObject.GetComponent<Bullet>().Speed = spd;
            obj.gameObject.GetComponent<Bullet>().OutCheck = true;
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * spd, ForceMode2D.Impulse);  
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.gameObject.GetComponent<Bullet>().Damge = dmg;
            newObj.gameObject.GetComponent<Bullet>().Speed = spd;
            newObj.gameObject.GetComponent<Bullet>().OutCheck = true;
            newObj.transform.SetParent(null);
            newObj.transform.position = Shoot.position;
            newObj.transform.rotation = Gun.rotation;
            newObj.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * spd, ForceMode2D.Impulse);
            return newObj;
        }
    }
    public static Bullet GetObject(Transform Shoot, int dmg, float spd, Vector2 direction)    //오브젝트 받아오기
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.transform.position = Shoot.position;

            obj.gameObject.SetActive(true);
            obj.gameObject.GetComponent<Bullet>().Damge = dmg;
            obj.gameObject.GetComponent<Bullet>().Speed = spd;
            obj.gameObject.GetComponent<Bullet>().OutCheck = true;
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * spd, ForceMode2D.Impulse);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.gameObject.GetComponent<Bullet>().Damge = dmg;
            newObj.gameObject.GetComponent<Bullet>().Speed = spd;
            newObj.gameObject.GetComponent<Bullet>().OutCheck = true;
            newObj.transform.SetParent(null);
            newObj.transform.position = Shoot.position;

            newObj.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * spd, ForceMode2D.Impulse);
            return newObj;
        }
    }
    public static void ReturnObject(Bullet obj)     //오브젝트 돌려받기
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}