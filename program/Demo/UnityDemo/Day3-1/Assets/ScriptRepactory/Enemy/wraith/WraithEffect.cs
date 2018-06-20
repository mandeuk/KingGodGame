using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithEffect : MonoBehaviour {
    public static WraithEffect instance = null;
    public List<GameObject> bulletList = new List<GameObject>();
    public List<GameObject> firedBulletList = new List<GameObject>();

    public List<GameObject> bulletHitList = new List<GameObject>();
    public List<GameObject> firedBulletHitList = new List<GameObject>();


    public GameObject spawnCloneList;

    // Use this for initialization
    void Awake() {
        instance = this;
        InitBullet();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireBullet(GameObject wraith)
    {
        GameObject bullet = bulletList[0];

        firedBulletList.Add(bullet);
        bulletList.RemoveAt(0);

        bullet.SetActive(true);
        bullet.transform.position = wraith.transform.position + transform.up * 0.5f;
        bullet.transform.rotation = wraith.transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(wraith.transform.forward * 200);
    }

    public void BulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        bulletList.Add(hitBullet);
        firedBulletList.Remove(hitBullet);

        PlayBulletHitEffect(hitBullet.transform);
    }

    public void PlayBulletHitEffect(Transform pos)
    {
        GameObject bulletHitEffect = bulletHitList[0];

        firedBulletHitList.Add(bulletHitList[0]);
        bulletHitList.RemoveAt(0);

        bulletHitEffect.SetActive(true);
        bulletHitEffect.transform.position = pos.position;
    }

    public void HitEffectVanishing(GameObject hitBulletEffect)
    {
        hitBulletEffect.SetActive(false);
        bulletHitList.Add(hitBulletEffect);
        firedBulletHitList.Remove(hitBulletEffect);
    }

    void InitBullet()
    {
        for (int i = 0; i < 30; i++)
        {
            bulletList.Add(SpawnBullet());
            //bulletHitList.Add(SpawnBulletHit());
        }
    }

    GameObject SpawnBullet()
    {
        GameObject bulletClone = Instantiate(Resources.Load("Prefabs/Enemy/WraithAttackBall"), spawnCloneList.transform) as GameObject;

        return bulletClone;
    }

    GameObject SpawnBulletHit()
    {
        GameObject bulletClone = Instantiate(Resources.Load("Prefabs/Effect/WraithBulletAttackHit"), spawnCloneList.transform) as GameObject;

        return bulletClone;
    }
}
