 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour {
    public List<Collider> targetList;
    public Vector3 movePos;

    int turnDir;

    // Use this for initialization
    void Awake()
    {
        targetList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<SlimeHealth>().isSinking)
                targetList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            TurnJudgeFunc();
            if (!transform.GetComponentInParent<EXMove>().onEXMove)
            Turn(movePos);
        }

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetComponent<SlimeHealth>().isSinking)
            {
                targetList.Remove(targetList[i]);
            }
        }
    }

    void Turn(Vector3 movePos)
    {
        transform.rotation = Quaternion.LookRotation(movePos.normalized);
    }

    public void TurnJudgeFunc()
    {
        if (Input.GetKey(KeyCode.A)) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
            movePos.Set(-1, 0, 1);

        if (Input.GetKey(KeyCode.D))
            movePos.Set(1, 0, -1);

        if (Input.GetKey(KeyCode.W))
            movePos.Set(1, 0, 1);

        if (Input.GetKey(KeyCode.S))
            movePos.Set(-1, 0, -1);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            movePos.Set(0, 0, 1);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            movePos.Set(1, 0, 0);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            movePos.Set(-1, 0, 0);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            movePos.Set(0, 0, -1);
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
