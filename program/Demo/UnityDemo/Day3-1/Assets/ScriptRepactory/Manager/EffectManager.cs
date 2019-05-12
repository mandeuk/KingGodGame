using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    public static EffectManager instance = null;
    //  playEffect(GameObject Caller, myDele A){
    //      Startcoroutain(A(Caller));
    //  }
    //  를 구현하고싶음.
    //  그걸 위한 델리게이트.. 처음 써봐서 어렵네...

    // 함수 구분 :  재사용이 잦은, 동시에 호출이 되는 이펙트는 리스트를 사용하여 오브젝트 풀링을 하게되는데
    //              이때는 코루틴을 사용하니 IEnumerator를 사용하고 1초뒤에 넣어주는방식으로 구현.
    //              동시호출이 없어 단발성으로 사용하는 경우에는 오브젝트 풀링이 필요없으므로
    //              void를 사용하여 함수만 호출하여 이펙트만 재생해줌.
    public delegate IEnumerator EffectIEnumeratorMethod(GameObject caller, int type);
    public delegate void EffectVoidMethod(GameObject caller);

    // public static void PlayEffect(effectKind effectMethod, GameObject caller) 요게 안됨 왜지.. static 문제인듯..
    // 함수 기능 :  이펙트를 재생시켜줌
    // 함수 특징 :  델리게이트로 받아서 콜백을 구현, 다른 인자값으로 오버라이딩을 해서 무슨 함수던지 유연하게 대처하게함.
    //              caller -> 이펙트를 주문한 사람, 이펙트의 위치
    //              type -> 이펙트의 타입, 보통 안쓰지만 특정한 처리를 하고싶을때 넘겨주는 변수
    //              effectMethod -> 부를 메소드의 이름.
    public void PlayEffect(GameObject caller, int type, EffectIEnumeratorMethod effectMethod)
    {
        StartCoroutine(effectMethod(caller, type));
    }

    public static void PlayEffect(GameObject caller, EffectVoidMethod effectMethod)
    {
        effectMethod(caller);
    }

    public GameObject player;
    
    //-------------------enemy-------------------//

    List<GameObject> enemyHitEffects = new List<GameObject>();
    List<GameObject> enemyUsedHitEffects = new List<GameObject>();

    List<GameObject> enemyWraithDeadEffects = new List<GameObject>();
    List<GameObject> enemyUsedWraithDeadEffects = new List<GameObject>();

    List<GameObject> enemyWraithWorriorDeadEffects = new List<GameObject>();
    List<GameObject> enemyUsedWraithWorriorDeadEffects = new List<GameObject>();

    List<GameObject> enemyWraithWorriorAttackEffects = new List<GameObject>();
    List<GameObject> enemyUsedWraithWorriorAttackEffects = new List<GameObject>();

    List<GameObject> enemyWraithBulletHitEffects = new List<GameObject>();
    List<GameObject> enemyUsedWraithBulletHitEffects = new List<GameObject>();

    List<GameObject> enemyHighPreistBulletHitEffect = new List<GameObject>();
    List<GameObject> enemyUsedHighPreistBulletHitEffect = new List<GameObject>();

    List<GameObject> SoulOfImpHitEffects = new List<GameObject>();
    List<GameObject> UsedSoulOfImpHitEffects = new List<GameObject>();

    List<GameObject> enemyTarBulletHitEffects = new List<GameObject>();
    List<GameObject> enemyUsedTarBulletHitEffects = new List<GameObject>();

    List<GameObject> enemyIceTarBulletHitEffects = new List<GameObject>();
    List<GameObject> enemyUsedIceTarBulletHitEffects = new List<GameObject>();

    static GameObject corosusJumpAttackEffect;
    static GameObject corosusBreakEffect;

    //-------------------player-------------------//

    List<GameObject> playerAttackEffects = new List<GameObject>();
    List<GameObject> playerUsedAttackEffects = new List<GameObject>();

    List<GameObject> playerDevilAttackEffects = new List<GameObject>();
    List<GameObject> playerUsedDevilAttackEffects = new List<GameObject>();

    List<GameObject> playerFootStepEffects = new List<GameObject>();
    List<GameObject> playerUsedFootStepEffects = new List<GameObject>();

    static GameObject playerSwordBlinkEffect; // 외부에서 직접 넣어주기 위해서 퍼블릭으로 작성.

    static GameObject playOuraEffect;

    static GameObject playerEXMoveSlashEffect;
    static GameObject playerEXmoveVanishEffect; 
    static GameObject playerEXMoveVanishFlowerEffect;
    static GameObject playerEXMoveRingEffectBack;
    static GameObject playerExMoveRingEffectFront;

    static GameObject playerChargeAttackEffect;
    public GameObject playerChargingEffect; // 외부에서(chargeAttack FBX 마지막) 꺼줘야해서 public으로 선언.
    static GameObject playerChargeEndEffect;

    static GameObject playerDodgeDustEffect;
    static GameObject playerGetLegendItemEffet;
    static GameObject playerGetMasterItemEffet;
    static GameObject playerGetRareItemEffet;
    static GameObject playerGetNormalItemEffet;

    GameObject stageClearDoor;

    //static GameObject playerEnergyApplyEffect;
    //static GameObject playerEtereApplyEffect;

    GameObject raphael;

    void Awake() {
        instance = this;
        raphael = PlayerBase.instance.gameObject;        
    }

    private void Start()
    {
        SpawnInit();
    }

    protected void SpawnInit()
    {
        player = PlayerBase.instance.gameObject;
        //playerSwordBlinkEffect = Instantiate(Resources.Load("Prefabs/Effect/BlinkEffect"), blinkEffectPos.transform) as GameObject;

        playerSwordBlinkEffect = GameObject.Find("BlinkEffect");

        playOuraEffect = GameObject.Find("OuraEffect");

        playerEXmoveVanishEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveVanishEffect")) as GameObject;
        playerEXMoveVanishFlowerEffect = Instantiate(Resources.Load("Prefabs/Effect/VanishFlowerEffect")) as GameObject;
        playerEXMoveRingEffectBack = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectBack")) as GameObject;
        playerExMoveRingEffectFront = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectFront")) as GameObject;
        playerEXMoveSlashEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveSlashEffectSimple")) as GameObject;

        playerChargeAttackEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEffect")) as GameObject;
        playerChargingEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackChargeEffect2")) as GameObject;
        playerChargeEndEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEndEffect")) as GameObject;

        playerGetLegendItemEffet = Instantiate(Resources.Load("Prefabs/Item/LegendItemGetEffect"), player.transform) as GameObject;
        playerGetMasterItemEffet = Instantiate(Resources.Load("Prefabs/Item/MasterItemGetEffect"), player.transform) as GameObject; 
        playerGetRareItemEffet = Instantiate(Resources.Load("Prefabs/Item/RareItemGetEffect"), player.transform) as GameObject; 
        playerGetNormalItemEffet = Instantiate(Resources.Load("Prefabs/Item/NormalItemGetEffect"), player.transform) as GameObject; 

        playerDodgeDustEffect = Instantiate(Resources.Load("Prefabs/Effect/DodgeDustEffect"), transform) as GameObject;

        corosusJumpAttackEffect = Instantiate(Resources.Load("Prefabs/Enemy/CorosusFrostJumpAttackEffect")) as GameObject;
        corosusBreakEffect = Instantiate(Resources.Load("Prefabs/Enemy/CorosusFrostBreakEffect")) as GameObject;

        stageClearDoor = Instantiate(Resources.Load("Prefabs/Map/StageClearDoorEffect")) as GameObject;

        //  1. 먹을지도 안먹을지도 모르는 아이템을 위해 처음부터 메모리공간 할당?
        //  2. 한번에 총알,피격이펙트 140개를 한프레임에 띄워서 분명 프레임드랍이 일어날게 뻔함. 그래도 먹을때 생성?
        //  2-1. 코루틴으로 프레임마다 10개씩 생성하는 방법도 있긴함.
        for(int i = 0; i < 250; i++)
        {
            GameObject HighPreistBulletHitEffect = Instantiate(Resources.Load("Prefabs/Effect/HighPreistBulletHit"), transform) as GameObject;

            enemyHighPreistBulletHitEffect.Add(HighPreistBulletHitEffect);
        }

        for (int i = 0; i < 70; i++)
        {
            GameObject wraithBulletHitEffect = Instantiate(Resources.Load("Prefabs/Effect/WraithBulletAttackHit"), transform) as GameObject;
            GameObject enemySoulOfImpHitEffectClone = Instantiate(Resources.Load("Prefabs/Effect/BallAttackEffect2"), transform) as GameObject;
            GameObject slashHitEffectClone = Instantiate(Resources.Load("Prefabs/Effect/SlashHitEffect2"), transform) as GameObject;

            enemyWraithBulletHitEffects.Add(wraithBulletHitEffect);
            SoulOfImpHitEffects.Add(enemySoulOfImpHitEffectClone);
            enemyHitEffects.Add(slashHitEffectClone);
        }

        for (int i = 0; i < 50; ++i)
        {
            GameObject TarBulletHitEffect = Instantiate(Resources.Load("Prefabs/Effect/TarBulletHitEffect"), transform) as GameObject;
            GameObject wraithDeadEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithDeadEffect"), transform) as GameObject;
            GameObject wraithWorriorDeadEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorDeadEffect"), transform) as GameObject;
            GameObject wraithWorriorAttackEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorAttackEffect"), transform) as GameObject;
            GameObject iceTarBulletHitEffect = Instantiate(Resources.Load("Prefabs/Effect/IceTarBulletHitEffect"), transform) as GameObject;

            enemyTarBulletHitEffects.Add(TarBulletHitEffect);
            enemyIceTarBulletHitEffects.Add(iceTarBulletHitEffect);
            enemyWraithDeadEffects.Add(wraithDeadEffectClone);
            enemyWraithWorriorDeadEffects.Add(wraithWorriorDeadEffectClone);
            enemyWraithWorriorAttackEffects.Add(wraithWorriorAttackEffectClone);
        }

        for (int i = 0; i<10; ++i)
        {
            GameObject slashEffectClone = Instantiate(Resources.Load("Prefabs/Effect/slash1"), transform) as GameObject;

            playerAttackEffects.Add(slashEffectClone);
        }

        for (int i = 0; i < 10; ++i)
        {
            GameObject slashEffectClone = Instantiate(Resources.Load("Prefabs/Effect/slash2"), transform) as GameObject;
            GameObject footStepEffectClone = Instantiate(Resources.Load("Prefabs/Effect/FootStepEffect"), transform) as GameObject;

            playerFootStepEffects.Add(footStepEffectClone);
            playerDevilAttackEffects.Add(slashEffectClone);
        }

    }

    public IEnumerator playEnemyWraithWorriorDeadEffect(GameObject caller, int type)
    {
        Vector3 pos = caller.transform.position;
        GameObject effectclone = enemyWraithWorriorDeadEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = pos + Vector3.up * 0.5f;

        enemyUsedWraithWorriorDeadEffects.Add(effectclone);
        enemyWraithWorriorDeadEffects.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);

        effectclone.SetActive(false);
        enemyWraithWorriorDeadEffects.Add(effectclone);
        enemyUsedWraithWorriorDeadEffects.Remove(effectclone);
        yield break;
    }

    //  stateNum : 5번은 임프아이템.
    public IEnumerator playEnemyHitEffect(GameObject caller, int stateNum)
    {
        if (stateNum == 5)
        {
            yield break;
        }

        GameObject effectclone = enemyHitEffects[0];

        effectclone.SetActive(true);
        effectclone.transform.position = caller.transform.position + Vector3.up * 0.7f;
        effectclone.transform.localScale = new Vector3(1f, 1f, 1f);
        effectclone.transform.LookAt(raphael.transform);
        effectclone.transform.Rotate(new Vector3(0, 0, Random.Range(-30, 30)));

        if (stateNum == 2)
        {
            effectclone.transform.Rotate(new Vector3(0, 0, 180));
        }

        if (stateNum == 4)
        {
            effectclone.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        enemyUsedHitEffects.Add(effectclone);
        enemyHitEffects.Remove(effectclone);
        yield return new WaitForSeconds(1.0f);

        effectclone.SetActive(false);
        enemyHitEffects.Add(effectclone);
        enemyUsedHitEffects.Remove(effectclone);
        yield break;
    }

    public IEnumerator playEnemyImpHitEffect(GameObject caller, int type)
    {
        GameObject effectclone = SoulOfImpHitEffects[0];

        effectclone.SetActive(true);
        effectclone.transform.position = caller.transform.position;

        UsedSoulOfImpHitEffects.Add(effectclone);
        SoulOfImpHitEffects.Remove(effectclone);

        yield return new WaitForSeconds(3.0f);

        effectclone.SetActive(false);
        SoulOfImpHitEffects.Add(effectclone);
        UsedSoulOfImpHitEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playEnemyWraithDeadEffect(GameObject caller, int type)
    {
        Vector3 pos = caller.transform.position;
        GameObject effectclone = enemyWraithDeadEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = pos + Vector3.up * 0.5f;

        enemyUsedWraithDeadEffects.Add(effectclone);
        enemyWraithDeadEffects.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyWraithDeadEffects.Add(effectclone);
        enemyUsedWraithDeadEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playEnemyWraithWorriorAttackEffect(GameObject caller, int type)
    {
        GameObject effectclone = enemyWraithWorriorAttackEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = caller.transform.position + Vector3.up * 0.5f;
        effectclone.transform.rotation = Quaternion.Euler(0, caller.transform.rotation.eulerAngles.y, 0);

        enemyUsedWraithWorriorAttackEffects.Add(effectclone);
        enemyWraithWorriorAttackEffects.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyWraithWorriorAttackEffects.Add(effectclone);
        enemyUsedWraithWorriorAttackEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playEnemyWraithBulletHitEffect(GameObject caller, int stateNum)
    {
        GameObject effectclone = enemyWraithBulletHitEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = caller.transform.position;


        enemyUsedWraithBulletHitEffects.Add(effectclone);
        enemyWraithBulletHitEffects.RemoveAt(0);

        yield return new WaitForSeconds(0.8f);
        effectclone.SetActive(false);
        enemyWraithBulletHitEffects.Add(effectclone);
        enemyUsedWraithBulletHitEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playPlyaerFootstepEffect(GameObject caller, int stateNum)
    {
        GameObject effectclone = playerFootStepEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.rotation = caller.transform.rotation;
        effectclone.transform.position = caller.transform.position + Vector3.up * 1f + caller.transform.forward * -0.8f;


        playerUsedFootStepEffects.Add(effectclone);
        playerFootStepEffects.RemoveAt(0);

        yield return new WaitForSeconds(0.5f);
        effectclone.SetActive(false);

        playerFootStepEffects.Add(effectclone);
        playerUsedFootStepEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playHighPreistBulletHitEffect(GameObject caller, int stateNum)
    {
        GameObject effectclone = enemyHighPreistBulletHitEffect[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = caller.transform.position;

        enemyUsedHighPreistBulletHitEffect.Add(effectclone);
        enemyHighPreistBulletHitEffect.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyHighPreistBulletHitEffect.Add(effectclone);
        enemyUsedHighPreistBulletHitEffect.Remove(effectclone);


        yield break;
    }

    public IEnumerator playEnemyTarBulletHitEffect(GameObject caller, int stateNum)
    {
        GameObject effectclone = enemyTarBulletHitEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = caller.transform.position;


        enemyUsedTarBulletHitEffects.Add(effectclone);
        enemyTarBulletHitEffects.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyTarBulletHitEffects.Add(effectclone);
        enemyUsedTarBulletHitEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playEnemyIceTarBulletHitEffect(GameObject caller, int stateNum)
    {
        GameObject effectclone = enemyIceTarBulletHitEffects[0];

        effectclone.SetActive(true);
        effectclone.GetComponent<ParticleSystem>().Play();
        effectclone.transform.position = caller.transform.position;


        enemyUsedIceTarBulletHitEffects.Add(effectclone);
        enemyIceTarBulletHitEffects.RemoveAt(0);

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyIceTarBulletHitEffects.Add(effectclone);
        enemyUsedIceTarBulletHitEffects.Remove(effectclone);

        yield break;
    }

    public IEnumerator playPlayerAttackEffect(GameObject caller, int stateNum)
    {
        GameObject effectClone;
        if (!PlayerBase.instance.isDevil)
        {
            effectClone = playerAttackEffects[0];

            if (stateNum == 1)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.397f
                    + caller.transform.up * .759f
                    + caller.transform.forward * .649f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-90, 180 + caller.transform.rotation.eulerAngles.y, 205);

            }

            else if (stateNum == 2)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.098f
                    + caller.transform.up * 0.861f
                    + caller.transform.forward * 0.373f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-110 - 180, 270 + caller.transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 3)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.18f
                    + caller.transform.up * 0.88f
                    + caller.transform.forward * 0.283f;
                effectClone.transform.rotation =
                    Quaternion.Euler(101 - 180, 270 + caller.transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 4)
            {
                effectClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * 0f
                    + caller.transform.up * 0.922f
                    + caller.transform.forward * 0.323f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-90, 180 + caller.transform.rotation.eulerAngles.y, 205);
            }

            effectClone.SetActive(true);
            playerUsedAttackEffects.Add(effectClone);
            playerAttackEffects.Remove(effectClone);
            SoundManager.playRaphaelNormalAttackSound();

            yield return new WaitForSeconds(0.6f);

            effectClone.SetActive(false);
            playerAttackEffects.Add(effectClone);
            playerUsedAttackEffects.Remove(effectClone);

        }
        else
        {
            effectClone = playerDevilAttackEffects[0];

            if (stateNum == 1)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.397f
                    + caller.transform.up * .759f
                    + caller.transform.forward * .649f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-90, 180 + caller.transform.rotation.eulerAngles.y, 205);

            }

            else if (stateNum == 2)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.098f
                    + caller.transform.up * 0.861f
                    + caller.transform.forward * 0.373f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-110 - 180, 270 + caller.transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 3)
            {
                effectClone.transform.localScale = new Vector3(1f, 1f, 1f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * -0.18f
                    + caller.transform.up * 0.88f
                    + caller.transform.forward * 0.283f;
                effectClone.transform.rotation =
                    Quaternion.Euler(101 - 180, 270 + caller.transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 4)
            {
                effectClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                effectClone.transform.position = caller.transform.position
                    + caller.transform.right * 0f
                    + caller.transform.up * 0.922f
                    + caller.transform.forward * 0.323f;
                effectClone.transform.rotation =
                    Quaternion.Euler(-90, 180 + caller.transform.rotation.eulerAngles.y, 205);
            }

            effectClone.SetActive(true);
            playerUsedDevilAttackEffects.Add(effectClone);
            playerDevilAttackEffects.Remove(effectClone);
            SoundManager.playRaphaelDarkAttackSound();

            yield return new WaitForSeconds(0.6f);

            effectClone.SetActive(false);
            playerDevilAttackEffects.Add(effectClone);
            playerUsedDevilAttackEffects.Remove(effectClone);

        }

        yield break;
    }

    public static void playPlayerOuraEffect()
    {
        playOuraEffect.SetActive(true);
        playOuraEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void stopPlayerOuraEffect()
    {
        playOuraEffect.GetComponent<ParticleSystem>().Stop();
    }

    public void playplayerSwordBlinkEffect()
    {
        playerSwordBlinkEffect.SetActive(true);
        playerSwordBlinkEffect.GetComponent<ParticleSystem>().Stop();
        playerSwordBlinkEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playDodgeDustEffect(GameObject caller)
    {
        playerDodgeDustEffect.SetActive(true);
        playerDodgeDustEffect.transform.position = caller.transform.position + Vector3.up;
        playerEXmoveVanishEffect.transform.rotation = Quaternion.Euler(0, caller.transform.rotation.eulerAngles.y, 0);
        playerDodgeDustEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playEXMoveSlashEffect(GameObject caller)
    {
        Vector3 EXPos = caller.GetComponentInChildren<SkillTarget>().movePos.normalized;

        playerEXMoveSlashEffect.SetActive(true);
        playerEXMoveSlashEffect.transform.position = caller.transform.position + EXPos * 2f + Vector3.up;
        playerEXMoveSlashEffect.transform.rotation = Quaternion.LookRotation(EXPos);
        playerEXMoveSlashEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.playRaphaelExMoveSound();
    }

    public static void playEXMoveVanishEffect(GameObject caller)
    {
        playerEXmoveVanishEffect.SetActive(true);
        playerEXmoveVanishEffect.transform.position = caller.transform.position + Vector3.up;
        playerEXmoveVanishEffect.transform.rotation = Quaternion.Euler(-90, caller.transform.rotation.eulerAngles.y, 0);
        playerEXmoveVanishEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playEXmoveVanishFlowerEffect(GameObject caller)
    {
        playerEXMoveVanishFlowerEffect.SetActive(true);
        playerEXMoveVanishFlowerEffect.transform.position = caller.transform.position + Vector3.up;
        playerEXMoveVanishFlowerEffect.transform.rotation = Quaternion.Euler(-142f, caller.transform.rotation.eulerAngles.y, 0);
        playerEXMoveVanishFlowerEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playExMoveRingEffectFront(GameObject caller)
    {
        Quaternion ExRotation = Quaternion.LookRotation(caller.GetComponentInChildren<SkillTarget>().movePos.normalized);

        playerExMoveRingEffectFront.SetActive(true);
        playerExMoveRingEffectFront.transform.position = caller.transform.position + Vector3.up;
        playerExMoveRingEffectFront.transform.rotation = Quaternion.Euler(0, ExRotation.eulerAngles.y, 0);
        playerExMoveRingEffectFront.GetComponent<ParticleSystem>().Play();
    }

    public static void playExMoveRingEffectBack(GameObject caller)
    {
        Quaternion ExRotation = Quaternion.LookRotation(caller.GetComponentInChildren<SkillTarget>().movePos.normalized);

        playerEXMoveRingEffectBack.SetActive(true);
        playerEXMoveRingEffectBack.transform.position = caller.transform.position + Vector3.up;
        playerEXMoveRingEffectBack.transform.rotation = Quaternion.Euler(0, ExRotation.eulerAngles.y, 0);
        playerEXMoveRingEffectBack.GetComponent<ParticleSystem>().Play();
    }

    public static void playChargeAttackEffect(GameObject caller)
    {
        playerChargeAttackEffect.SetActive(false);
        playerChargeAttackEffect.SetActive(true);
        playerChargeAttackEffect.transform.position = caller.transform.position + Vector3.up * 0.1f;
        playerChargeAttackEffect.transform.rotation = Quaternion.Euler(-90, caller.transform.rotation.eulerAngles.y, 0);
        playerChargeAttackEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.playRaphaelChargeAttackSound();
    }

    public void playplayerChargingEffect(GameObject caller)
    {
        playerChargingEffect.SetActive(false);
        playerChargingEffect.SetActive(true);
        playerChargingEffect.transform.position = caller.transform.position + Vector3.up * 1.4f - caller.transform.forward * 0.15f;
        playerChargingEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.playRaphaelChargeAttackChargeSound();
    }

    public void playChargeAttackEndEffect(GameObject caller)
    {
        playerChargingEffect.SetActive(false);
        playerChargeAttackEffect.SetActive(false);
        playerChargeEndEffect.SetActive(false);
        playerChargeEndEffect.SetActive(true);
        playerChargeEndEffect.transform.position = caller.transform.position + Vector3.up;
        playerChargeEndEffect.GetComponent<ParticleSystem>().Play();
    }

    public void stageClearDoorEffect(Vector3 roomPos)
    {
        stageClearDoor.transform.position = roomPos + Vector3.up * 2;
        stageClearDoor.SetActive(true);
    }

    public static void playGetLegendItemEffet()
    {
        playerGetLegendItemEffet.SetActive(false);
        playerGetLegendItemEffet.SetActive(true);
        playerGetLegendItemEffet.transform.position = PlayerBase.instance.transform.position + Vector3.up;
        SoundManager.playLegendItemGet();
    }

    public static void playGetMasterItemEffet()
    {
        playerGetMasterItemEffet.SetActive(false);
        playerGetMasterItemEffet.SetActive(true);
        playerGetMasterItemEffet.transform.position = PlayerBase.instance.transform.position + Vector3.up;
        SoundManager.playMasterItemGet();
    }

    public static void playGetRareItemEffet()
    {
        playerGetRareItemEffet.SetActive(false);
        playerGetRareItemEffet.SetActive(true);
        playerGetRareItemEffet.transform.position = PlayerBase.instance.transform.position + Vector3.up;
    }

    public static void playGetNormalItemEffect()
    {
        playerGetNormalItemEffet.SetActive(false);
        playerGetNormalItemEffet.SetActive(true);
        playerGetNormalItemEffet.transform.position = PlayerBase.instance.transform.position + Vector3.up;
    }

    public static void playCorosusBreakEffect(GameObject pos)
    {
        corosusBreakEffect.transform.position = pos.transform.position + pos.transform.forward * 3.5f + Vector3.up * 2f;
        corosusBreakEffect.transform.rotation = Quaternion.Euler(0, pos.transform.rotation.y + 180, 0);
        corosusBreakEffect.SetActive(true);
        corosusBreakEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playCorosusJumpAttackEffect(GameObject pos)
    {
        corosusJumpAttackEffect.transform.position = pos.transform.position + pos.transform.forward * 2.0f + Vector3.up * 0.2f;
        corosusJumpAttackEffect.SetActive(true);
        corosusJumpAttackEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.playCorosusJumpAttack();
    }

    //public static void playEnergyApplyEffect(GameObject caller)
    //{
    //    playerEnergyApplyEffect.SetActive(false);
    //    playerEnergyApplyEffect.SetActive(true);
    //    playerEnergyApplyEffect.transform.position = caller.transform.position;
    //    playerEnergyApplyEffect.GetComponent<ParticleSystem>().Play();
    //}

    //public static void playEtereApplyEffect(GameObject caller)
    //{
    //    playerEtereApplyEffect.SetActive(false);
    //    playerEtereApplyEffect.SetActive(true);
    //    playerEtereApplyEffect.transform.position = caller.transform.position;
    //    playerEtereApplyEffect.GetComponent<ParticleSystem>().Play();
    //}

    //------------------------------Bullet------------------------------//
}
