using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{    
    public virtual void playBlinkEffect()
    {
        EffectManager.instance.playplayerSwordBlinkEffect();
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
}
