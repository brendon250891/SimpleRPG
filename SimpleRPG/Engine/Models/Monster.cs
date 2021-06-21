using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : BaseNotification
    {
        #region Private Properties

        private int _hitPoints;

        #endregion

        #region Public Properties

        public string Name { get; private set; }
        public string ImageName { get; set; }
        public int MaximumHitPoints { get; private set; }
        public int HitPoints
        {
            get { return _hitPoints; }
            private set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExperiencePoints { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        #endregion

        public Monster(string name, string imageName, int maximumHitPoints, int hitPoints, int minimumDamage, int maximumDamage, int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}.png";
            MaximumHitPoints = maximumHitPoints;
            HitPoints = hitPoints;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;

            Inventory = new();
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
        }
    } 
}
