using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Private Properties

        private int _experiencePoints;

        #endregion

        #region Public Properties

        public string CharacterClass { get; set; }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;

                OnPropertyChanged();
                SetLevelAndMaximumHitPoints();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }

        #endregion

        #region Events

        public event EventHandler OnLeveledUp;

        #endregion

        public Player(string name, string characterClass, int maximumHitPoints, int currentHitPoints, int experiencePoints, int level, int gold)
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Level = level;

            Quests = new ObservableCollection<QuestStatus>();
            Recipes = new ObservableCollection<Recipe>();
        }

        #region Public Methods

        public void GainExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        #endregion

        #region Private Methods

        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;

                OnLeveledUp?.Invoke(this, new System.EventArgs());
            }
        }

        #endregion
    }
}
