using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    public GameObject blinkEffect;
    public GameObject OuraEffect;

    public virtual void playBlinkEffect()
    {
        blinkEffect.GetComponent<ParticleSystem>().Stop();
        blinkEffect.GetComponent<ParticleSystem>().Play();
        //EffectManager.instance.playplayerSwordBlinkEffect();
    }

    public virtual void playOuraEffect()
    {
        OuraEffect.GetComponent<ParticleSystem>().Play();
    }

    public virtual void stopOuraEffect()
    {
        OuraEffect.GetComponent<ParticleSystem>().Stop();
    }
    
    public virtual void playExplosionAttackEffect()
    {
        EffectManager.PlayEffect(gameObject, EffectManager.playChargeAttackEffect);
    }

    public virtual void playChargeAttackChargeEffect()
    {
        EffectManager.PlayEffect(gameObject, EffectManager.instance.playplayerChargingEffect);
    }

    public virtual void playChargeAttackEndEffect()
    {
        EffectManager.PlayEffect(gameObject, EffectManager.instance.playChargeAttackEndEffect);
    }

    public virtual void playFootstepEffect()
    {
        if(transform.tag == "Player")
            EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playPlyaerFootstepEffect);
    }
}
