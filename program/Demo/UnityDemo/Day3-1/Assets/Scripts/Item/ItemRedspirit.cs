using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemRedspirit : MonoBehaviour
{
    public class Redspirit
    {

    }

    public void Effect()
    {
        PlayerStatus.healthPoint += 0.5f;
    }
}
