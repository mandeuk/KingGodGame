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

    // 플레이어는 Damaged 를 따로 구현해준다.
    // 플레이어는 1초의 무적시간이 필요하기때문에 isDamaged 상태값을 이용해서 무적시간을 주기 위함.
    public override void Damaged(DamageNode damageNode)
    {
        if (!entity.isDamaged && !entity.isInvincibility)
        {
            StartCoroutine(NormalDamaged(damageNode));
        }
    }

    public override IEnumerator NormalDamaged(DamageNode damageNode)
    {
        entity.isDamaged = true;
        TakeDamage(damageNode);

        yield return new WaitForSeconds(1.0f);
        if (!entity.isDead)
        {
            AfterDamage(damageNode);
        }
        entity.isDamaged = false;

        yield break;
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        if (!playerEntity.isInvincibility && !playerEntity.isDead)
        {
            PlayerColorChange.instance.PlayerColorChangeBlack();
            PlayerColorChange.instance.RimColorRed();
            CameraEventManager.instance.VignetteColorRedChange();
            PlayerBase.instance.SetStatus(damageNode.damage, false, PlayerBase.instance.CurHP);
            base.TakeDamage(damageNode);
            if (PlaySceneUIManager.instance != null)
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
        SoundManager.instance.StopEnvironmentalBGM();
        SoundManager.instance.StopStageBGM();
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
