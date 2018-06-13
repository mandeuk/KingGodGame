using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTarget : MonoBehaviour {
    public List<Collider> targetList;
    public GameObject wind;
    GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Etere"))
        {
            targetList.Add(other);
            StartCoroutine(GetEtere(other.gameObject));
        }
    }

    // Use this for initialization
    void Awake()
    {
        targetList = new List<Collider>();
        player = PlayerBase.instance.gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (targetList.Count < 0)
        {
            //wind.SetActive(false);
        }

		for(int i = 0; i<targetList.Count; i++)
        {
            if(!targetList[i].GetComponent<ParticleSystem>().isPlaying)
            {

                targetList.RemoveAt(i);
            }
        }
	}

    public IEnumerator GetEtere(GameObject other)
    {
        ParticleSystem.ExternalForcesModule fmodule;
        fmodule = other.GetComponent<ParticleSystem>().externalForces;
        fmodule.multiplier = 1;

        //yield return new WaitForSeconds(1.3f);
        //fmodule.multiplier = 1;
        //wind.SetActive(true);

        yield return new WaitForSeconds(1f);
        PlayerBase.instance.etere += 4;
        PlaySceneUIManager.instance.ChangeEterAmountText();
        other.GetComponent<ParticleSystem>().Stop();
        EnergyManager.instance.ReturnEtere(other);
        EffectManager.instance.PlayEffect(gameObject, EffectManager.instance.playEtereApplyEffect);
        yield break;
    }
}
