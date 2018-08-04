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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Damaged(new DamageNode(1, this.gameObject, 0.4f, 1, 1));
        }
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        if (!playerEntity.isInvincibility && !playerEntity.isDead)
        {
            PlayerColorChange.instance.PlayerColorChangeBlack();
            PlayerColorChange.instance.RimColorRed();
            PlayerBase.instance.SetStatus(damageNode.damage, false, PlayerBase.instance.CurHP);
            print(damageNode.damage);
            print(damageNode.attacker);
            base.TakeDamage(damageNode);
            PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
        }
    }

    public override void AfterDamage(DamageNode damageNode)
    {
        PlayerColorChange.instance.RimColorOrigin();
        base.AfterDamage(damageNode);
    }

    public override void Death()
    {
        base.Death();

        anim.SetTrigger("Die");
        PlayerBase.instance.PlayerDisable();
        GameManager.NewGameStart();

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
