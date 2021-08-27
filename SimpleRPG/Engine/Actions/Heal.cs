using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class Heal : BaseAction, IAction
    {
        #region Private Properties

        private readonly int _hitPointsToHeal;

        #endregion

        public Heal(GameItem itemInUse, int hitPointsToHeal) : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable.");
            }

            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} points.");
            target.Heal(_hitPointsToHeal);
        }
    }
}
