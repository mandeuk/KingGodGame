using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour {
    public static EnergyManager instance = null;
    public GameObject SpawnCloneList;
    
    List<GameObject> energyCloneList = new List<GameObject>();
    List<GameObject> usedEnergyCloneList = new List<GameObject>();

    List<GameObject> etereCloneList = new List<GameObject>();
    List<GameObject> usedEtereCloneList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        InitEnergy();
    }

    public void InitEnergy()
    {
        for (int i = 0; i < 30; ++i)
        {
            energyCloneList.Add(SpawnEnergy());
            etereCloneList.Add(SpawnEtere());
        }
    }

    public GameObject SpawnEnergy()
    {
        GameObject energy = Instantiate(Resources.Load("Prefabs/EnergyBall"), SpawnCloneList.transform) as GameObject;

        return energy;
    }


    public GameObject SpawnEtere()
    {
        GameObject energy = Instantiate(Resources.Load("Prefabs/Effect/EterePop"), SpawnCloneList.transform) as GameObject;

        return energy;
    }

    public GameObject SpawnLotus()
    {
        GameObject lotus = Instantiate(Resources.Load("Prefabs/Effect/ItemFieldEffect"), SpawnCloneList.transform) as GameObject;

        return lotus;
    }

    public void DropEnergy(Vector3 pos)
    {
        GameObject energyBall = energyCloneList[0];

        usedEnergyCloneList.Add(energyBall);
        energyCloneList.RemoveAt(0);

        energyBall.SetActive(true);
        energyBall.GetComponent<ParticleSystem>().Play();
        energyBall.transform.position = pos + Vector3.up * 0.6f;
        energyBall.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
    }

    public void returnEnergy(GameObject other)
    {
        GameObject energyBall = other;

        energyBall.SetActive(false);
        energyCloneList.Add(energyBall);
        usedEnergyCloneList.Remove(energyBall);
    }

    public void DropEtere(Vector3 pos)
    {
        GameObject etere = etereCloneList[0];

        usedEtereCloneList.Add(etere);
        etereCloneList.RemoveAt(0);

        etere.SetActive(true);
        etere.GetComponent<ParticleSystem>().Play();
        etere.transform.position = pos + Vector3.up * 0.6f;
    }
    
    public void ReturnEtere(GameObject other)
    {
        GameObject etere = other;

        etere.SetActive(false);
        etereCloneList.Add(etere);
        usedEtereCloneList.Remove(etere);
    }
}
