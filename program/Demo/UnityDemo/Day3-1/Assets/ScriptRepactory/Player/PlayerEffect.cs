using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    EffectManager effect;
    
    public virtual void playBlinkEffect()
    {
        effect.PlayEffect(gameObject, effect.playplayerSwordBlinkEffect);
    }
    
    public virtual void playExplosionAttackEffect()
    {
        effect.PlayEffect(gameObject, effect.playChargeAttackEffect);
    }

    public virtual void playChargeAttackChargeEffect()
    {
        effect.PlayEffect(gameObject, effect.playplayerChargingEffect);
    }
    public virtual void playChargeAttackEndEffect()
    {
        effect.PlayEffect(gameObject, effect.playChargeAttackEndEffect);
    }

    void Awake()
    {
        effect = EffectManager.instance;        
    }
}
