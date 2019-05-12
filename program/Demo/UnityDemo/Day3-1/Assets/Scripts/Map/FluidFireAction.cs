using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidFireAction : MonoBehaviour {
    public GameObject[] open = new GameObject[2];
    public GameObject[] close = new GameObject[2];

    public void DoorOpen()
    {
        open[0].SetActive(true);
        open[1].SetActive(true);
        close[0].SetActive(false);
        close[1].SetActive(false);
    }

    public void DoorClose()
    {
        open[0].SetActive(false);
        open[1].SetActive(false);
        close[0].SetActive(true);
        close[1].SetActive(true);
    }

    public void DoorEmpty()
    {
        open[0].SetActive(false);
        open[1].SetActive(false);
        close[0].SetActive(false);
        close[1].SetActive(false);
    }
}
