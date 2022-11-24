using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public abstract void UseSkill(Transform maincam,Player player,Action<GameObject> callback= null);
}
