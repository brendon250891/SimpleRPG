using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon : IAction
    {
        #region Private Properties

        private readonly GameItem _weapon;

        private readonly int _minimumDamage;

        private readonly int _maximumDamage;

        #endregion

        #region Events

        public event EventHandler<string> OnActionPerformed;

        #endregion

        public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage)
        {
            if (weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }

            if (minimumDamage < 0)
            {
                throw new ArgumentException("Minimum Damage mus tbe 0 or larger.");
            }

            if (maximumDamage < minimumDamage)
            {
                throw new ArgumentException("Maximum Damage must be >= Minimum Damage");
            }

            _weapon = weapon;
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);

            string actorName = (actor is Player) ? "You" : $"The {actor.Name}";
            string targetName = (target is Player) ? "you" : $"the {actor.Name}";

            if (damage == 0)
            {
                ReportResult($"{actorName} missed the {targetName}.");
            }
            else
            {
                ReportResult($"{actorName} hit the {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);
            }
        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
