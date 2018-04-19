using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhealth : MonoBehaviour {
    public float startingHealth = 100;  //EnnemyData 라는 파일 만들어서 관리해야함.
    float currentHealth;

    public float flashSpeed = 2f;

    public GameObject player;

    Material[] enemyMat;
    Color[] enemyMatOrigColor;
    Animator enemyAnim;
    Rigidbody enemyRigidBody;

    public bool isDead = false;
    public bool damaged = false;
    bool isExMove = false;
   

    // Use this for initialization
    void Awake () {
        enemyMat = new Material[GetComponentsInChildren<Renderer>().Length];
        enemyMatOrigColor = new Color[GetComponentsInChildren<Renderer>().Length];

        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            enemyMat[i] = GetComponentsInChildren<Renderer>()[i].material;
            enemyMatOrigColor[i] = enemyMat[i].color;
        }

        //int num = GetComponentsInChildren<Renderer>().Length;
       // print(num);

        //foreach(Renderer mats in GetComponentsInChildren<Renderer>())
        //    enemyMat[0] = mats.material;
        
        // 위의 매터리얼은 몬스터에따라 바뀌어야하지만 일단 임의로 작성함.
        // 매터리얼이 하나일지 두개일지 모르니까.

        enemyRigidBody = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        currentHealth = startingHealth;
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 적과 플레이어 사이에 장애물이 있는지 체크함.

    public bool CheckBtwRapObj()
    {
        Vector3 diff = player.transform.position - transform.position;
        float dist = Vector3.Distance(player.transform.position, transform.position);

        RaycastHit[] colList = Physics.RaycastAll(transform.position, new Vector3(diff.x, 0f, diff.z).normalized, dist);

        for (int i = 0; i < colList.Length; i++)
        {
            if (colList[i].transform.CompareTag("Obstacle"))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        enemyRigidBody.isKinematic = true;
        StartCoroutine(ColorChangeDie());

        //몬스터사망 후 아이템 생성
        //ItemManager.Instance.SpawnItem(gameObject.transform.position);

        Destroy(gameObject, .8f);
    }

    public IEnumerator NormalDamaged(float damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        //Time.timeScale = .5f;
        TakeDamage(damage);
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            enemyMat[i].SetColor("_Color", new Vector4(.2f, .2f, .2f, 1));
        }
        Vector3 diff = playerPosition - transform.position;
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(stateNum));

        yield return new WaitForSeconds(delay);
        if (!isDead)
        {
            enemyRigidBody.Sleep();
            StartCoroutine(ColorChange());
        }
        damaged = false;
        //Time.timeScale = 1;
        yield break;
    }

    public IEnumerator SkillDamaged(float damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        damaged = true;
        //isExMove = true;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            enemyMat[i].SetColor("_Color", new Vector4(.2f, .2f, .2f, 1));
        }
        enemyAnim.speed = 0;
        Vector3 diff = playerPosition - transform.position;
        enemyRigidBody.Sleep();

        yield return new WaitForSeconds(delay);
        TakeDamage(damage);
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 560f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(5));

        yield return new WaitForSeconds(.2f);
        enemyRigidBody.Sleep();
        if (!isDead)
        {
            enemyAnim.speed = 1;
            StartCoroutine(ColorChange());
        }
        damaged = false;
        yield break;
    }

    public IEnumerator ColorChange()
    {
        float timer = new float();
        while (timer < 3)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.Lerp(enemyMat[i].GetColor("_Color"), enemyMatOrigColor[i], flashSpeed * Time.deltaTime));
            }
        }

        yield break;
    }

    public IEnumerator ColorChangeDie()
    {
        float timer = new float();
        while (timer < 3)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.Lerp(enemyMat[i].GetColor("_Color"), Color.white * 3, Time.deltaTime * flashSpeed * 3));
            }
        }

        yield break;
    }
}