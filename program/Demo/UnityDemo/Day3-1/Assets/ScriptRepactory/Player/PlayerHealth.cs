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
        PlayerStatus.instance.PlayerDisable();

        Invoke("CameraFadeOut", 2);
        Invoke("loadScene", 6);
    }

    protected override void Init()
    {
        base.Init();
        anim = GetComponent<Animator>();
        playerEntity = entity as PlayerBase;
    }

    void CameraFadeOut()
    {
        PlaySceneUIManager.instance.UIFadeOut();
    }

    void loadScene()
    {
        SceneManager.LoadScene("Game_Junghoon");
    }

    void loadMenuScene()
    {
        SceneManager.LoadScene("GameIntromenu");
    }

    public void PlayerDamaged(GameObject Hit)
    {

    }
}
