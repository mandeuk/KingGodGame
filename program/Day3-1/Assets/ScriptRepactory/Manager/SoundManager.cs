using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BGMChange();

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    static AudioSource bgmAudio;
    static AudioSource clipAudio;
    static AudioSource enviroAudio;
    static AudioClip[] myClips = new AudioClip[110];
    static float clipVolume = 0.4f;

    private void Awake()
    {
        InitSound();
    }

    void InitSound()
    {
        instance = this;
        bgmAudio = gameObject.AddComponent<AudioSource>();
        bgmAudio.loop = true;
        bgmAudio.priority = 100;
        enviroAudio = gameObject.AddComponent<AudioSource>();
        enviroAudio.loop = true;
        enviroAudio.priority = 50;
        clipAudio = gameObject.AddComponent<AudioSource>();
        clipAudio.priority = 200; 


        myClips[0] = Instantiate(Resources.Load("Sound/BGM/Stage1BGM")) as AudioClip;
        myClips[1] = Instantiate(Resources.Load("Sound/BGM/Stage2BGM")) as AudioClip;
        myClips[2] = Instantiate(Resources.Load("Sound/BGM/CorosusBGM")) as AudioClip;
        myClips[3] = Instantiate(Resources.Load("Sound/BGM/HighPriestBGM")) as AudioClip;


        myClips[4] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_attack1")) as AudioClip;
        myClips[5] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_attack2")) as AudioClip;

        myClips[6] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_chaging1")) as AudioClip;
        myClips[7] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_chaging2")) as AudioClip;
        myClips[8] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_chaging3")) as AudioClip;
        myClips[9] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_chaging4")) as AudioClip;

        myClips[10] = Instantiate(Resources.Load("Sound/Raphael/raphael_darkattack_slash1")) as AudioClip;
        myClips[11] = Instantiate(Resources.Load("Sound/Raphael/raphael_darkattack_slash2")) as AudioClip;
        myClips[12] = Instantiate(Resources.Load("Sound/Raphael/raphael_darkattack_slash3")) as AudioClip;


        myClips[13] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash4")) as AudioClip;
        myClips[14] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash5")) as AudioClip;
        myClips[15] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash6")) as AudioClip;

        myClips[16] = Instantiate(Resources.Load("Sound/Raphael/raphael_normalattack_slash1")) as AudioClip;
        myClips[17] = Instantiate(Resources.Load("Sound/Raphael/raphael_normalattack_slash2")) as AudioClip;
        myClips[18] = Instantiate(Resources.Load("Sound/Raphael/raphael_normalattack_slash3")) as AudioClip;
        myClips[19] = Instantiate(Resources.Load("Sound/Raphael/raphael_normalattack_slash4")) as AudioClip;



        myClips[20] = Instantiate(Resources.Load("Sound/Item/activeitem_lotusofabyss_shoot1")) as AudioClip;
        myClips[21] = Instantiate(Resources.Load("Sound/Item/activeitem_lotusofabyss_shoot2")) as AudioClip;
        myClips[22] = Instantiate(Resources.Load("Sound/Item/activeitem_lotusofabyss_shoot3")) as AudioClip;

        myClips[23] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_shoot1")) as AudioClip;
        myClips[24] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_shoot2")) as AudioClip;
        myClips[25] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_shoot3")) as AudioClip;
        myClips[26] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_shoot4")) as AudioClip;
        myClips[27] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_shoot5")) as AudioClip;

        myClips[28] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Legenditem_get1")) as AudioClip;
        myClips[29] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Legenditem_get2")) as AudioClip;
        myClips[30] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Legenditem_get3")) as AudioClip;

        myClips[31] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Masteritem_get1")) as AudioClip;
        myClips[32] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Masteritem_get2")) as AudioClip;
        myClips[33] = Instantiate(Resources.Load("Sound/Item/fieldtiem_Masteritem_get3")) as AudioClip;

        myClips[34] = Instantiate(Resources.Load("Sound/Item/item_energy_get1")) as AudioClip;
        myClips[35] = Instantiate(Resources.Load("Sound/Item/item_energy_get2")) as AudioClip;

        myClips[36] = Instantiate(Resources.Load("Sound/Item/item_etere_get1")) as AudioClip;
        myClips[37] = Instantiate(Resources.Load("Sound/Item/item_etere_get2")) as AudioClip;
        myClips[38] = Instantiate(Resources.Load("Sound/Item/item_etere_get3")) as AudioClip;
        myClips[39] = Instantiate(Resources.Load("Sound/Item/item_etere_get4")) as AudioClip;



        myClips[40] = Instantiate(Resources.Load("Sound/Enemy/monster_normal_hit1")) as AudioClip;
        myClips[41] = Instantiate(Resources.Load("Sound/Enemy/monster_normal_hit2")) as AudioClip;
        myClips[42] = Instantiate(Resources.Load("Sound/Enemy/monster_normal_hit3")) as AudioClip;
        myClips[43] = Instantiate(Resources.Load("Sound/Enemy/monster_normal_hit4")) as AudioClip;

        myClips[44] = Instantiate(Resources.Load("Sound/Enemy/monster_still_hit1")) as AudioClip;
        myClips[45] = Instantiate(Resources.Load("Sound/Enemy/monster_still_hit2")) as AudioClip;
        myClips[46] = Instantiate(Resources.Load("Sound/Enemy/monster_still_hit3")) as AudioClip;

        myClips[47] = Instantiate(Resources.Load("Sound/Enemy/monster_stone_hit1")) as AudioClip;
        myClips[48] = Instantiate(Resources.Load("Sound/Enemy/monster_stone_hit2")) as AudioClip;
        myClips[49] = Instantiate(Resources.Load("Sound/Enemy/monster_stone_hit3")) as AudioClip;
        myClips[50] = Instantiate(Resources.Load("Sound/Enemy/monster_stone_hit4")) as AudioClip;


        myClips[51] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_attack1")) as AudioClip;
        myClips[52] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_attack2")) as AudioClip;

        myClips[53] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_laser3")) as AudioClip;
        myClips[54] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_laser4")) as AudioClip;

        myClips[55] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_jumpattack1")) as AudioClip;
        myClips[56] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_pootwalk1")) as AudioClip;

        myClips[57] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash1")) as AudioClip;
        myClips[58] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash2")) as AudioClip;
        myClips[59] = Instantiate(Resources.Load("Sound/Raphael/raphael_exmove_slash3")) as AudioClip;

        myClips[60] = Instantiate(Resources.Load("Sound/ETC/environment_stage1")) as AudioClip;
        myClips[61] = Instantiate(Resources.Load("Sound/ETC/environment_stage2")) as AudioClip;

        myClips[62] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_break1")) as AudioClip;
        myClips[63] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_break2")) as AudioClip;
        myClips[64] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_break3")) as AudioClip;

        myClips[65] = Instantiate(Resources.Load("Sound/Raphael/raphael_dodge8")) as AudioClip;
        myClips[66] = Instantiate(Resources.Load("Sound/Raphael/raphael_dodge9")) as AudioClip;

        myClips[67] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_bulletattack1")) as AudioClip;

        myClips[68] = Instantiate(Resources.Load("Sound/Raphael/raphael_chargeattack_attack1-1")) as AudioClip;

        myClips[69] = Instantiate(Resources.Load("Sound/ETC/twitch_apply1")) as AudioClip;
        //myClips[70] = Instantiate(Resources.Load("Sound/ETC/twitch_apply2")) as AudioClip;


        myClips[71] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_hit1")) as AudioClip;
        myClips[72] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_hit2")) as AudioClip;
        myClips[73] = Instantiate(Resources.Load("Sound/Item/activeitem_soulofimp_hit3")) as AudioClip;

        myClips[74] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_dying1")) as AudioClip;
        myClips[75] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_dying2")) as AudioClip;

        myClips[76] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_attackready1")) as AudioClip;
        myClips[78] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_attackready2")) as AudioClip;
        myClips[79] = Instantiate(Resources.Load("Sound/Enemy/Corosus/corosus_attackready3")) as AudioClip;

        myClips[80] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_death1")) as AudioClip;
        myClips[81] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_death2")) as AudioClip;

        myClips[82] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraithworrior_attack1")) as AudioClip;
        myClips[83] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraithworrior_attack1")) as AudioClip;

        myClips[84] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_normalattack1")) as AudioClip;
        myClips[85] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_normalattack2")) as AudioClip;
        myClips[86] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_normalattack3")) as AudioClip;
        myClips[87] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_normalattack4")) as AudioClip;
        myClips[88] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_normalattack5")) as AudioClip;

        myClips[89] = Instantiate(Resources.Load("Sound/Enemy/HighPriest/highpriest_bulletattack1")) as AudioClip;

        myClips[90] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_death1-1")) as AudioClip;
        myClips[91] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_death1-2")) as AudioClip;
        myClips[92] = Instantiate(Resources.Load("Sound/Enemy/Wraith/wraith_death1-3")) as AudioClip;
        
        myClips[93] = Instantiate(Resources.Load("Sound/Item/item_drop1")) as AudioClip;

        myClips[94] = Instantiate(Resources.Load("Sound/ETC/door_close1")) as AudioClip;
        myClips[95] = Instantiate(Resources.Load("Sound/ETC/door_open1")) as AudioClip;

        myClips[96] = Instantiate(Resources.Load("Sound/ETC/title_burn1")) as AudioClip;
    }

    public void playNewBGM(BGMChange bgm, AudioSource audio, double volume)
    {
        StartCoroutine(changeNewBGM(bgm, audio, volume));
    }

    IEnumerator changeNewBGM(BGMChange bgm, AudioSource audio, double volume)
    {
        while (audio.volume > 0)
        {
            audio.volume -= 0.015f;
            yield return null;
        }

        bgm();

        while (audio.volume < volume)
        {
            audio.volume += 0.015f;
            yield return null;
        }

        yield break;
    }

    void StopBGM(AudioSource audio)
    {
        StartCoroutine(stopCurBGM(audio));
    }

    IEnumerator stopCurBGM(AudioSource audio)
    {
        while (audio.volume > 0)
        {
            audio.volume -= 0.015f;
            yield return null;
        }
        audio.volume = 0;
        yield break;
    }

    public void playStage1Music()
    {
        playNewBGM(Stage1Bgm, bgmAudio, 0.5f);
        playNewBGM(stage1Environment, enviroAudio, 0.1f);
    }

    public void playStage2Music()
    {
        playNewBGM(Stage2Bgm, bgmAudio, 0.5f);
        playNewBGM(Stage2Environment, enviroAudio, 0.2f);
    }

    public void playCorosusMusic()
    {
        playNewBGM(CorosusBGM, bgmAudio, 0.5f);
    }

    public void playHighPriestMusic()
    {
        playNewBGM(HighPriestBGM, bgmAudio, 0.5f);
    }

    public static void Stage1Bgm()
    {
        bgmAudio.clip = myClips[0];
        bgmAudio.PlayOneShot(myClips[0]);
        //bgmAudio.Play();        
    }

    public void StopStageBGM()
    {
        StopBGM(bgmAudio);
    }

    public void StopEnvironmentalBGM()
    {
        StopBGM(enviroAudio);
    }

    public static void stage1Environment()
    {
        enviroAudio.clip = myClips[60];
        enviroAudio.PlayOneShot(myClips[60]);
        //enviroAudio.Play();
    }

    static void Stage2Bgm()
    {
        bgmAudio.clip = myClips[1];        
        bgmAudio.Play();        
    }

    static void Stage2Environment()
    {
        enviroAudio.clip = myClips[61];
        enviroAudio.Play();
    }

    static void CorosusBGM()
    {
        bgmAudio.clip = myClips[2];
        bgmAudio.Play();
    }

    static void HighPriestBGM()
    {
        bgmAudio.clip = myClips[3];
        bgmAudio.Play();
    }

    public static void playRaphaelChargeAttackSound()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 4], clipVolume);
    }

    public static void playRaphaelChargeAttackChargeSound()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 4) + 6], clipVolume);
        clipAudio.PlayOneShot(myClips[68], clipVolume);
    }

    public static void playRaphaelDarkAttackSound()
    {

        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 10], clipVolume);
    }

    public static void playRaphaelExMoveSound()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 13] , clipVolume);
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 57], clipVolume - 0.2f);
    }

    public static void playRaphaelNormalAttackSound()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 16], clipVolume);
    }

    public static void playRaphaelDodge()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 65], clipVolume);
    }


    public static void playItemLotusOfAbyssShoot()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 20], clipVolume);
    }

    public static void playSoulOfImpShoot()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 5) + 23], clipVolume - 0.25f);
    }

    public static void playSoulOfImpHit()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 71], clipVolume - 0.2f);
    }

    public static void playLegendItemGet()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 28], clipVolume);
    }

    public static void playMasterItemGet()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 31], clipVolume);
    }

    public static void playEnergyGet()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 34], clipVolume);
    }

    public static void playEtereGet()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 36], clipVolume);
    }

    public static void playEnemyNormalHit()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 4) + 40], clipVolume);
    }

    public static void playEnemyStillHit()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 44], clipVolume);
    }

    public static void playEnemyStoneHit()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 47], clipVolume);
    }

    public static void playWraithAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 51], clipVolume);
    }

    public static void playWraithDying()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 80], clipVolume);
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 90], clipVolume);
    }

    public static void playWraithWorriorAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 82], clipVolume);
    }

    public static void playHighPriestLaserAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 53], clipVolume);
    }

    public static void playHighPriestNormalAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 5) + 84], clipVolume + 0.2f);
    }

    public static void playHighPriestWildAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 89], clipVolume + 0.1f);
    }

    public static void playCorosusJumpAttack()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 55], clipVolume + 0.2f);
    }

    public static void playCorosusFootWalk()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 56], clipVolume + 0.1f);
    }

    public static void playCorosusBreak()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 62], clipVolume + 0.2f);
    }

    public static void playCorosusDying()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 2) + 74], clipVolume + 0.2f);
    }

    public static void playCorosusAttackReady()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 76], clipVolume);
    }

    public static void playTwitchApply()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 69], clipVolume);
    }

    public static void playItemDropSound()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 93], clipVolume + 0.3f);
    }

    public static void playDoorClose()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 94], clipVolume - 0.2f);
    }

    public static void playDoorOpen()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 1) + 95], clipVolume - 0.2f);
    }

    public static void play()
    {
        clipAudio.PlayOneShot(myClips[Random.Range(0, 3) + 31], clipVolume);
    }
}
