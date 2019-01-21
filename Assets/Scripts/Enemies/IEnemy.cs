using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    interface IEnemy
    {
        void Stop();
        void Damage(float damageAmount);
    }
}
