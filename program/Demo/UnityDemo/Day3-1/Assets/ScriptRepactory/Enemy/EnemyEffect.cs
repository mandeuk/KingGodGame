using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour {
    public static EnemyEffect instance = null;
    public GameObject spawnCloneList;

    public List<GameObject> hitcloneList = new List<GameObject>();
    public List<GameObject> usedhitcloneList = new List<GameObject>();

    public List<GameObject> wraithDeadEffect = new List<GameObject>();
    public List<GameObject> usedWraithDeadEffect = new List<GameObject>();

    public List<GameObject> wraithWorriorDeadEffect = new List<GameObject>();
    public List<GameObject> usedWraithWorriorDeadEffect = new List<GameObject>();

    public List<GameObject> wraithWorriorAttackEffect = new List<GameObject>();
    public List<GameObject> usedWraithWorriorAttackEffect = new List<GameObject>();

    GameObject raphael;

    public IEnumerator PlayEffect(int stateNum , GameObject enemy)
    {
        usedhitcloneList.Add(hitcloneList[0]);
        hitcloneList[0].SetActive(true);
        hitcloneList[0].transform.position = enemy.transform.position + Vector3.up*0.7f;
        hitcloneList[0].transform.localScale = new Vector3(1f, 1f, 1f);
        hitcloneList[0].transform.LookAt(raphael.transform);
        hitcloneList[0].transform.Rotate(new Vector3(0, 0, Random.Range(-30,30)));

        if(stateNum == 2)
        {
            hitcloneList[0].transform.Rotate(new Vector3(0, 0, 180));
        }

        if(stateNum == 4)
        {
            hitcloneList[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        if(stateNum == 5)
        {
            hitcloneList[0].transform.localScale = new Vector3(2f, 2f, 2f);
        }

        hitcloneList.RemoveAt(0);
        yield break;
    }

    public IEnumerator PlayWraithWorriorAttackEffect(GameObject enemy)
    {
        GameObject effectclone = wraithWorriorAttackEffect[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.rotation = Quaternion.Euler(0, enemy.transform.rotation.eulerAngles.y, 0);
        effectclone.transform.position = enemy.transform.position + Vector3.up * 0.5f;


        usedWraithWorriorAttackEffect.Add(effectclone);
        wraithWorriorAttackEffect.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        wraithWorriorAttackEffect.Add(effectclone);
        usedWraithWorriorAttackEffect.Remove(effectclone);


        yield break;
    }

    public void effectVanishing(GameObject effect)
    {
        effect.SetActive(false);
        hitcloneList.Add(effect);
        usedhitcloneList.Remove(effect);
    }

    public void PlayWraithDeadEffect(Vector3 pos)
    {
        GameObject effectclone = wraithDeadEffect[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = pos + Vector3.up * 0.5f;

        usedWraithDeadEffect.Add(effectclone);
        wraithDeadEffect.RemoveAt(0);
    }

    public void WraithDeadEffectVanishing(GameObject effect)
    {
        effect.SetActive(false);
        wraithDeadEffect.Add(effect);
        usedWraithDeadEffect.Remove(effect);
    }

    public void PlayWraithWorriorDeadEffect(Vector3 pos)
    {
        GameObject effectclone = wraithWorriorDeadEffect[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = pos + Vector3.up * 0.5f;

        usedWraithWorriorDeadEffect.Add(effectclone);
        wraithWorriorDeadEffect.RemoveAt(0);
    }

    public void WraithWorriorDeadEffectVanishing(GameObject effect)
    {
        effect.SetActive(false);
        wraithWorriorDeadEffect.Add(effect);
        usedWraithWorriorDeadEffect.Remove(effect);
    }

    // Use this for initialization
    void Awake()
    {
        instance = this;
        raphael = GameObject.FindWithTag("Player");
        InitEffect();
    }

    public void InitEffect()
    {
        for(int i =0; i<40; ++i)
        {
            hitcloneList.Add(SpawnEffect());
        }

        for (int i = 0; i < 30; ++i)
        {
            wraithDeadEffect.Add(SpawnWraithDeadEffect());
            wraithWorriorDeadEffect.Add(SpawnWraithWorriorDeadEffect());
            wraithWorriorAttackEffect.Add(SpawnWraithWorriorAttackEffect());
        }
    }

    public GameObject SpawnEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/SlashHitEffect") , spawnCloneList.transform) as GameObject;

        return effectClone;
    }

    public GameObject SpawnWraithDeadEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithDeadEffect"), spawnCloneList.transform) as GameObject;

        return effectClone;
    }

    public GameObject SpawnWraithWorriorDeadEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorDeadEffect"), spawnCloneList.transform) as GameObject;

        return effectClone;
    }

    public GameObject SpawnWraithWorriorAttackEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorAttackEffect"), spawnCloneList.transform) as GameObject;

        return effectClone;
    }
}