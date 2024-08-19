using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : HealthBase, IDamageable
{
    public UIFillUpdater healthBar;

    public override void Damage(int damage)
    {
        base.Damage(damage);
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthBar?.UpdateValueEmpty(currLife, soHealth.maxLife);
    }
}
