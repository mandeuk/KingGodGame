using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase {
    Animator anim;
    PlayerBase playerEntity;

	// Use this for initialization
	void Awake () {
        Init();
    }    

    public override void TakeDamage(DamageNode damageNode)
    {
        if (!playerEntity.isInvincibility && !playerEntity.isDead)
        {
            StopCoroutine(PlayerColorChange.instance.ColorChange());
            StartCoroutine(PlayerColorChange.instance.ColorChange());
            PlayerBase.instance.SetStatus(damageNode.damage, false, PlayerBase.instance.CurHP);
            base.TakeDamage(damageNode);
            PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
        }
    }

    public override void AfterDamage(DamageNode damageNode)
    {
        base.AfterDamage(damageNode);
    }

    public override void Death()
    {
        base.Death();

        anim.SetTrigger("Die");
        PlayerBase.instance.PlayerDisable();

        Invoke("CameraFadeOut", 2);
        Invoke("loadMenuScene", 6);
    }

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void CameraFadeOut()
    {
        PlaySceneUIManager.instance.UIFadeOut();
    }

    void loadScene()
    {
        SceneManager.LoadScene("Game_Stage1");
    }

    void loadMenuScene()
    {
        SceneManager.LoadScene("GameIntromenu");
    }
}
