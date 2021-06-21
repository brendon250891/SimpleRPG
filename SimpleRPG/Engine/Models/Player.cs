using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : BaseNotification
    {
        #region Private Properties

        private int _experiencePoints;
        private int _hitPoints;
        private int _level;
        private int _gold;

        #endregion

        #region Public Properties

        public string Name { get; set; }
        public string CharacterClass { get; set; }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
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

        public ObservableCollection<QuestStatus> Quests { get; set; }

        public List<GameItem> Weapons => Inventory.Where(gameItem => gameItem is Weapon).ToList();

        #endregion

        public Player(string name, string characterClass, int hitPoints, int experiencePoints, int level, int gold)
        {
            Name = name;
            CharacterClass = characterClass;
            HitPoints = hitPoints;
            ExperiencePoints = experiencePoints;
            Level = level;
            Gold = gold;

            Inventory = new();
            Quests = new();
        }

        #region Public Methods

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            OnPropertyChanged(nameof(Weapons));
        }

        public void AddExperiencePoints(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void AddGold(int gold)
        {
            Gold += gold;
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
        }

        public void Respawn(int hitPoints)
        {
            HitPoints = hitPoints;
        }

        #endregion
    }
}
