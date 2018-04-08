using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
    public List<Items> ItemLists;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemLists.Add(other.GetComponent<Items>());
            other.GetComponent<Items>().Effect();
        }
    }

    private void Awake()
    {
        ItemLists = new List<Items>();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
