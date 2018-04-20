using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    public static BulletManager Instance;
    public static BulletManager getInStance()
    {
        return Instance;
    }

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    public enum BulletType
    {
        One=1,
        Two
    }

    public BulletType bulletType = BulletType.One;

    private float bulletLaunchTime = 0.3f;

    public int BulletNumber=10;

    public int m=0;

    Transform gun;

    private void Start()
    {
        gun = GameObject.Find("Tank/TankRenderers/TankTurret").transform;
        InvokeRepeating("CreateBullet", 1,0.3f);
    }

    void CreateBullet()
    {
       // yield return new WaitForSeconds(bulletLaunchTime);
        m = ++m;
        m = m % BulletNumber;
        Transform parent;
        if (!GameObject.Find("LinShi"))
        {
            parent = new GameObject("LinShi").transform;
        }
        parent = GameObject.Find("LinShi").transform;
        ObjectPool<Bullet>.getInstance().GetOut(m.ToString(),bulletType,parent,gun);
        ObjectPool<Bullet>.getInstance().SetIn(m.ToString(), ObjectPool<Bullet>.getInstance().prefab);
    }

}

public class ObjectPool<T> where T:Bullet,new()
{
    public static ObjectPool<T> instance;
    public static ObjectPool<T> getInstance()
    {
        if(instance==null)
        {
            instance = new ObjectPool<T>();
        }
        return instance;
    }

    public GameObject prefab;

    private Dictionary<string, List<T>> pool = new Dictionary<string, List<T>>();

    //读取对象池
    public void GetOut(string objectName,BulletManager.BulletType bulletType,Transform parent=null,Transform gun=null)
    {
        Bullet pre;
        if(pool.ContainsKey(objectName)&&pool[objectName].Count>0&&pool[objectName][0].bulletWorkType==Bullet.BulletWorkType.Idle)//如果存在子弹是关闭状态
        {
            pre = pool[objectName][0];
            pre.transform.parent = parent;
            pre.transform.position = BulletPosition(gun);//gun.position;
            pre.transform.rotation = pre.m_transfrom.rotation;
            pre.gameObject.SetActive(true);
            BulletEmission(pre.transform,gun.transform);
        }
        else
        {
            if(!pool.ContainsKey(objectName))
            {
                GameObject gameObject = GameObject.Instantiate(Resources.Load(((int)bulletType).ToString())) as GameObject;             
                gameObject.GetComponent<Bullet>().bulletWorkType = Bullet.BulletWorkType.Work;
                gameObject.transform.parent = parent;
                gameObject.name = objectName;
                gameObject.transform.position = BulletPosition(gun);//gun.position;
                gameObject.transform.rotation= gameObject.GetComponent<Bullet>().m_transfrom.rotation;
                prefab = gameObject;
                BulletEmission(gameObject.transform, gun.transform);
            }
        }
    }

    Vector3 BulletPosition(Transform gun)
    {
        Vector3 m;
        m= gun.position+ gun.forward * 0.5f;
        m=gun.position + gun.up * 0.5f;
        return m;
    }



    void BulletEmission(Transform bullet,Transform gun)
    {
        Quaternion rotation = Quaternion.LookRotation(gun.forward);

        bullet.transform.rotation = rotation;

        //bullet.localEulerAngles = new Vector3(-90,0,0);
    }

    //存入对象池
    public void SetIn(string objectName,GameObject bullet)
    {
        if(!pool.ContainsKey(objectName))
        {
            pool.Add(objectName,new List<T>());
            pool[objectName].Add(bullet.GetComponent<T>());
        }
    }


    public void DestoryPool(string objectName)
    {
        if(pool.ContainsKey(objectName))
        {
            pool[objectName] = null;
            pool.Remove(objectName);
        }
    }
}
