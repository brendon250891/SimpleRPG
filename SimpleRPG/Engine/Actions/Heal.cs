using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class Heal : IAction
    {
        #region Private Properties

        private readonly GameItem _item;

        private readonly int _hitPointsToHeal;

        #endregion

        #region Events

        public event EventHandler<string> OnActionPerformed;

        #endregion

        public Heal(GameItem item, int hitPointsToHeal)
        {
            if (item.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{item.Name} is not consumable.");
            }

            _item = item;
            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} points.");
            target.Heal(_hitPointsToHeal);
        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
