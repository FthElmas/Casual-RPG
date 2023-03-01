using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;


namespace RPG.Core
{
    public interface IAttack
    {
        void Attack(GameObject combatTarget);
    }
}