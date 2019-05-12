using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOfImp : MonoBehaviour {
    public GameObject bullet;

    [SerializeField]
    protected List<GameObject> bulletList = new List<GameObject>();

    [SerializeField]
    protected List<GameObject> firedBulletList = new List<GameObject>();

    PlayerBase playerEntity;
    DamageNode damageNode;
    NormalTarget normalTarget;
    List<Collider> targetList;

    private void OnEnable()
    {
        InitBullet();
        Init();
    }

    protected void Init()
    {
        playerEntity = PlayerBase.instance;
        //EventManager.AttackEventCall += new PlayerAttackEventHandler(Fire);
        EventManager.EnemyHitEventCall += new EnemyHitEventHandler(Fire);

        damageNode = new DamageNode(10, playerEntity.gameObject, 0.5f, 0, 5);
        normalTarget = playerEntity.GetComponentInChildren<NormalTarget>();
        //targetList = new List<Collider>(normalTarget.targetList);
    }

    public void Fire(int stateNum, GameObject enemy)
    {
        //targetList = new List<Collider>(normalTarget.targetList);

        //foreach (Collider target in targetList)
        if(!(stateNum == 5))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject bullet = bulletList[0];
                bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
                bullet.GetComponent<BulletBase>().Attacker = gameObject;
                bullet.GetComponent<SoulOfImpBullet>().target = enemy;

                firedBulletList.Add(bullet);
                bulletList.RemoveAt(0);
                bullet.SetActive(true);

                bullet.transform.position = transform.position
                    + transform.transform.right * Random.Range(-1f, 1f)
                    + transform.transform.up * Random.Range(1f, 2f)
                    - transform.transform.forward * Random.Range(2f, 2.5f);
                SoundManager.playSoulOfImpShoot();
            }
        }
    }

    public void BulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        bulletList.Add(hitBullet);
        firedBulletList.Remove(hitBullet);
    }

    public void InitBullet()
    {
        for (int i = 0; i < 70; i++)
        {
            GameObject bulletclone = Instantiate(bullet) as GameObject;

            bulletList.Add(bulletclone);
        }
    }

    private void OnDisable()
    {
        EventManager.EnemyHitEventCall -= new EnemyHitEventHandler(Fire);
    }


}
