using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithEffect : MonoBehaviour {
    public static WraithEffect instance = null;
    public List<GameObject> bulletList = new List<GameObject>();
    public List<GameObject> firedBulletList = new List<GameObject>();
    public GameObject spawnCloneList;

    // Use this for initialization
    void Awake() {
        instance = this;
        InitBullet();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FireBulletCoroutain(GameObject wraith)
    {
        GameObject bullet = bulletList[0];
                                                                                                                                                                                                                                                                                
        firedBulletList.Add(bullet);
        bulletList.Remove(bulletList[0]);

        bullet.SetActive(true);
        bullet.transform.position = wraith.transform.position + transform.up * 0.5f;
        bullet.GetComponent<Rigidbody>().AddForce(wraith.transform.forward * 200);
        yield break;
    }

    public void BulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        bulletList.Add(hitBullet);
        firedBulletList.Remove(hitBullet);
    }


    void InitBullet()
    {
        for (int i = 0; i < 10; i++)
        {
            bulletList.Add(SpawnBullet());
        }
    }

    GameObject SpawnBullet()
    {
        GameObject bulletClone = Instantiate(Resources.Load("Prefabs/WraithAttackBall"), spawnCloneList.transform) as GameObject;

        return bulletClone;
    }
}
