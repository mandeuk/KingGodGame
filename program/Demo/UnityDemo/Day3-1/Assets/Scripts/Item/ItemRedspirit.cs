using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemRedspirit : MonoBehaviour
{

    public void Effect()
    {
        PlayerStatus.instance.healthPoint += 0.5f;
    }
}
