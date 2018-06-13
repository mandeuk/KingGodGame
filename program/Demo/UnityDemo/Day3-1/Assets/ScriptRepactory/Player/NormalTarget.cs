using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour {
    public List<Collider> targetList = new List<Collider>();
    public List<Collider> anotherTargetList = new List<Collider>();
    public List<Collider> enemyBulletList = new List<Collider>();

	// Use this for initialization
	void Awake () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyWorrior"))
        {
            if (!other.GetComponent<EnemyBase>().isDead)
            {
                //other.GetComponent<EnemyBase>().Damaged();
                targetList.Add(other);
            }
        }


        if (other.CompareTag("EnemyBullet"))
        {
            enemyBulletList.Add(other);
        }

        if (other.CompareTag("Obstacle"))
        {
            anotherTargetList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyWorrior"))
        {
            if (!other.GetComponent<ObjectBase>().isDead)
                targetList.Remove(other);
        }

        if (other.CompareTag("Obstacle"))
        {
            anotherTargetList.Remove(other);
        }
        if (other.CompareTag("EnemyBullet"))
        {
            enemyBulletList.Remove(other);
        }
    }

    // Update is called once per frame
    void Update (){    // 여기 타겟 다 바꿔야됨
                        // 아마 건너뛰어서 검사하고있을거임
                        // i-- 식으로 설계해야함.
        for(int i = 0; i<targetList.Count; i++)
        {
            if (targetList[i].GetComponent<ObjectBase>().isDead)
            {
                targetList.Remove(targetList[i]);
            }
        }

        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].GetComponent<WraithBullet>().onFire)
            {
                enemyBulletList.Remove(enemyBulletList[i]);
            }
        }
    }
}
