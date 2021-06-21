using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Private Properties

        private int _experiencePoints;

        private int _level;

        #endregion

        #region Public Properties

        public string CharacterClass { get; set; }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }

        #endregion

        public Player(string name, string characterClass, int maximumHitPoints, int currentHitPoints, int experiencePoints, int level, int gold) 
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Level = level;

            Quests = new ObservableCollection<QuestStatus>();
        }

        #region Public Methods

        public bool HasAllItems(List<ItemQuantity> items)
        {
            foreach(ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        public void GainExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        #endregion
    }
}
