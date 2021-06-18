﻿using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Player : BaseNotification
    { 
        private int _experiencePoints;
        private int _hitPoints;
        private int _level;
        private int _gold;

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

        public Player(string name, string characterClass, int hitPoints, int experiencePoints, int level, int gold)
        {
            Name = name;
            CharacterClass = characterClass;
            HitPoints = hitPoints;
            ExperiencePoints = experiencePoints;
            Level = level;
            Gold = gold;

            Inventory = new();
        }
    }
}
