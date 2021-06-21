using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotification
    {
        #region Private Properties

        private string _name;

        private int _currentHitPoints;

        private int _maximumHitPoints;

        private int _gold;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;

                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;

                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;

                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(item => item is Weapon).ToList();

        #endregion

        protected LivingEntity()
        {
            Inventory = new();
        }

        #region Public Methods

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
        }

        public bool IsAlive()
        {
            return CurrentHitPoints > 0;
        }

        public void Respawn()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void Damage(int damage)
        {
            CurrentHitPoints -= damage;
        }

        #endregion
    }
}
