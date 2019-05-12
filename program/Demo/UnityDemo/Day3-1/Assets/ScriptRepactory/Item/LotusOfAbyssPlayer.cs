using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyssPlayer : MonoBehaviour{
    public GameObject bullet;
    [SerializeField]
    protected List<GameObject> bulletList = new List<GameObject>();

    [SerializeField]
    protected List<GameObject> firedBulletList = new List<GameObject>();

    PlayerBase playerEntity;
    DamageNode damageNode;

    private void OnEnable()
    {
        InitBullet();
        Init();
    }

    protected void Init()
    {
        playerEntity = PlayerBase.instance;
        EventManager.AttackEventCall += new PlayerAttackEventHandler(Fire);
        damageNode = new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.5f, playerEntity.pushBack, 1);
    }

    public void Fire(int stateNum)
    {
        GameObject bullet = bulletList[0];
        bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
        bullet.GetComponent<BulletBase>().Attacker = gameObject;

        firedBulletList.Add(bullet);
        bulletList.RemoveAt(0);
        bullet.SetActive(true);

        if (stateNum == 1)
        {
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);
            bullet.transform.position = transform.position
                + transform.right * -0.397f
                + transform.up * .759f
                + (transform.forward) * .649f;
            bullet.transform.rotation =
                Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        else if (stateNum == 2)
        {
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);
            bullet.transform.position = transform.position
                + transform.right * -0.098f
                + transform.up * 0.861f
                + (transform.forward) * 0.373f;
            bullet.transform.rotation =
                Quaternion.Euler(0, transform.rotation.eulerAngles.y, -25);
        }

        else if (stateNum == 3)
        {
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);
            bullet.transform.position = transform.position
                + transform.right * -0.18f
                + transform.up * 0.88f
                + (transform.forward) * 0.283f;
            bullet.transform.rotation =
                Quaternion.Euler(0, transform.rotation.eulerAngles.y, 25);
        }

        else if (stateNum == 4)
        {
            bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            bullet.transform.position = transform.position
                + transform.right * 0f
                + transform.up * 0.922f
                + transform.forward * 0.323f;
            bullet.transform.rotation =
                Quaternion.Euler(0,transform.rotation.eulerAngles.y, 0);
        }
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 550);
        SoundManager.playItemLotusOfAbyssShoot();
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
        for (int i = 0; i < 30; i++)
        {
            GameObject bulletclone = Instantiate(bullet) as GameObject;

            bulletList.Add(bulletclone);
        }
    }

    private void OnDisable()
    {
        EventManager.AttackEventCall -= new PlayerAttackEventHandler(Fire);
    }
}
