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

    public List<GameObject> enemyWraithBullets = new List<GameObject>();
    public List<GameObject> enemyUsedWraithBullets = new List<GameObject>();


    //-------------------player-------------------//

    List<GameObject> playerAttackEffects = new List<GameObject>();
    List<GameObject> playerUsedAttackEffects = new List<GameObject>();

    public GameObject playerSwordBlinkEffect; // 외부에서 직접 넣어주기 위해서 퍼블릭으로 작성.

    static GameObject playerEXMoveSlashEffect;
    static GameObject playerEXmoveVanishEffect;
    static GameObject playerEXMoveVanishFlowerEffect;
    static GameObject playerEXMoveRingEffectBack;
    static GameObject playerExMoveRingEffectFront;

    static GameObject playerChargeAttackEffect;
    public GameObject playerChargingEffect; // 외부에서(chargeAttack FBX 마지막) 꺼줘야해서 public으로 선언.
    static GameObject playerChargeEndEffect;

    public GameObject playerDodgeDustEffect;

    static GameObject playerEnergyApplyEffect;
    static GameObject playerEtereApplyEffect;

    GameObject raphael;

    void Awake() {
        instance = this;
        raphael = PlayerBase.instance.gameObject;
        //DeleInit();
        SpawnInit();
    }

    // 함수 기능 : 델리게이트 초기화
    // 왜 안해줘도 됨?;;;; 뭔데이거?;;
    protected void DeleInit()
    {
        EffectIEnumeratorMethod PlayEnemyWraithWorriorDeadEffect
            = new EffectIEnumeratorMethod(playEnemyWraithWorriorDeadEffect);
        EffectIEnumeratorMethod PlayEnemyHitEffect
            = new EffectIEnumeratorMethod(playEnemyHitEffect);
        EffectIEnumeratorMethod PlayEnemyWraithDeadEffect
            = new EffectIEnumeratorMethod(playEnemyWraithDeadEffect);
        EffectIEnumeratorMethod PlayEnemyWraithWorriorAttackEffect
            = new EffectIEnumeratorMethod(playEnemyWraithWorriorAttackEffect);

        EffectIEnumeratorMethod PlayPlayerAttackEffect
            = new EffectIEnumeratorMethod(playPlayerAttackEffect);

        EffectVoidMethod PlayEXMoveSlashEffect
            = new EffectVoidMethod(playEXMoveSlashEffect);
        EffectVoidMethod PlayEXMoveVanishEffect
            = new EffectVoidMethod(playEXMoveVanishEffect);
        EffectVoidMethod PlayEXmoveVanishFlowerEffect
            = new EffectVoidMethod(playEXmoveVanishFlowerEffect);
        EffectVoidMethod PlayExMoveRingEffectFront
            = new EffectVoidMethod(playExMoveRingEffectFront);
        EffectVoidMethod PlayExMoveRingEffectBack
            = new EffectVoidMethod(playExMoveRingEffectBack);

        EffectVoidMethod PlayChargeAttackEffect
            = new EffectVoidMethod(playChargeAttackEffect);
        EffectVoidMethod PlayplayerChargingEffect
            = new EffectVoidMethod(playplayerChargingEffect);
        EffectVoidMethod PlayChargeAttackEndEffect
            = new EffectVoidMethod(playChargeAttackEndEffect);

        EffectVoidMethod PlayEnergyApplyEffect
            = new EffectVoidMethod(playEnergyApplyEffect);
        EffectVoidMethod PlayEtereApplyEffect
            = new EffectVoidMethod(playEtereApplyEffect);
    }

    protected void SpawnInit()
    {
        //playerSwordBlinkEffect = Instantiate(Resources.Load("Prefabs/Effect/BlinkEffect"), blinkEffectPos.transform) as GameObject;

        playerEXmoveVanishEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveVanishEffect"), transform) as GameObject;
        playerEXMoveVanishFlowerEffect = Instantiate(Resources.Load("Prefabs/Effect/VanishFlowerEffect"), transform) as GameObject;
        playerEXMoveRingEffectBack = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectBack"), transform) as GameObject;
        playerExMoveRingEffectFront = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectFront"), transform) as GameObject;
        playerEXMoveSlashEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveSlashEffectSimple"), transform) as GameObject;

        playerChargeAttackEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEffect"), transform) as GameObject;
        playerChargingEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackChargeEffect2"), transform) as GameObject;
        playerChargeEndEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEndEffect"), transform) as GameObject;

        playerEnergyApplyEffect = Instantiate(Resources.Load("Prefabs/Effect/EnergyGetEffect"), transform) as GameObject;
        playerEtereApplyEffect = Instantiate(Resources.Load("Prefabs/Effect/EtereGetEffect"), transform) as GameObject;

        
        for (int i = 0; i < 40; ++i)
        {
            GameObject slashHitEffectClone = Instantiate(Resources.Load("Prefabs/Effect/SlashHitEffect"), transform) as GameObject;

            enemyHitEffects.Add(slashHitEffectClone);
        }

        for (int i = 0; i < 30; ++i)
        {
            GameObject wraithDeadEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithDeadEffect"), transform) as GameObject;
            GameObject wraithWorriorDeadEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorDeadEffect"), transform) as GameObject;
            GameObject wraithWorriorAttackEffectClone = Instantiate(Resources.Load("Prefabs/Effect/WraithWorriorAttackEffect"), transform) as GameObject;
            GameObject wraithBulletHitEffect = Instantiate(Resources.Load("Prefabs/Effect/WraithBulletAttackHit"), transform) as GameObject;
            GameObject wraithBullet = Instantiate(Resources.Load("Prefabs/Enemy/WraithAttackBall"), transform) as GameObject;

            enemyWraithDeadEffects.Add(wraithDeadEffectClone);
            enemyWraithWorriorDeadEffects.Add(wraithWorriorDeadEffectClone);
            enemyWraithWorriorAttackEffects.Add(wraithWorriorAttackEffectClone);
            enemyWraithBulletHitEffects.Add(wraithBulletHitEffect);
            enemyWraithBullets.Add(wraithBullet);
        }

        for (int i = 0; i<10; ++i)
        {
            GameObject slashEffectClone = Instantiate(Resources.Load("Prefabs/Effect/slash1"), transform) as GameObject;
            playerAttackEffects.Add(slashEffectClone);
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

    public IEnumerator playEnemyHitEffect(GameObject caller, int stateNum)
    {
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

        if (stateNum == 5)
        {
            effectclone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        enemyUsedHitEffects.Add(effectclone);
        enemyHitEffects.Remove(effectclone);
        yield return new WaitForSeconds(1.0f);

        effectclone.SetActive(false);
        enemyHitEffects.Add(effectclone);
        enemyUsedHitEffects.Remove(effectclone);
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

        yield return new WaitForSeconds(1.0f);
        effectclone.SetActive(false);
        enemyWraithBulletHitEffects.Add(effectclone);
        enemyUsedWraithBulletHitEffects.Remove(effectclone);


        yield break;
    }

    public IEnumerator playPlayerAttackEffect(GameObject caller, int stateNum)
    {
        GameObject effectClone = playerAttackEffects[0];

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

        yield return new WaitForSeconds(0.6f);

        effectClone.SetActive(false);
        playerAttackEffects.Add(effectClone);
        playerUsedAttackEffects.Remove(effectClone);

        yield break;
    }

    public void playplayerSwordBlinkEffect()
    {
        playerSwordBlinkEffect.SetActive(true);
        playerSwordBlinkEffect.GetComponent<ParticleSystem>().Play();
    }

    public void playDodgeDustEffect()
    {
        playerDodgeDustEffect.SetActive(true);
        playerDodgeDustEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playEXMoveSlashEffect(GameObject caller)
    {
        Vector3 EXPos = caller.GetComponentInChildren<SkillTarget>().movePos.normalized;

        playerEXMoveSlashEffect.SetActive(true);
        playerEXMoveSlashEffect.transform.position = caller.transform.position + EXPos * 2f + Vector3.up;
        playerEXMoveSlashEffect.transform.rotation = Quaternion.LookRotation(EXPos);
        playerEXMoveSlashEffect.GetComponent<ParticleSystem>().Play();
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
    }

    public void playplayerChargingEffect(GameObject caller)
    {
        playerChargingEffect.SetActive(false);
        playerChargingEffect.SetActive(true);
        playerChargingEffect.transform.position = caller.transform.position + Vector3.up * 1.4f - caller.transform.forward * 0.15f;
        playerChargingEffect.GetComponent<ParticleSystem>().Play();
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

    public static void playEnergyApplyEffect(GameObject caller)
    {
        playerEnergyApplyEffect.SetActive(false);
        playerEnergyApplyEffect.SetActive(true);
        playerEnergyApplyEffect.transform.position = caller.transform.position;
        playerEnergyApplyEffect.GetComponent<ParticleSystem>().Play();
    }

    public static void playEtereApplyEffect(GameObject caller)
    {
        playerEtereApplyEffect.SetActive(false);
        playerEtereApplyEffect.SetActive(true);
        playerEtereApplyEffect.transform.position = caller.transform.position;
        playerEtereApplyEffect.GetComponent<ParticleSystem>().Play();
    }

    //------------------------------Bullet------------------------------//

    public void WraithBulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        enemyWraithBullets.Add(hitBullet);
        enemyUsedWraithBullets.Remove(hitBullet);
    }
}
