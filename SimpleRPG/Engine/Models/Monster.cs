namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        #region Public Properties

        public string ImageName { get; set; }

        public int MinimumDamage { get; set; }

        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }

        #endregion

        public Monster(string name, string imageName, int maximumHitPoints, int minimumDamage, int maximumDamage, int rewardExperiencePoints, int gold)
        {
            Name = name;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}.png";
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = maximumHitPoints;
            RewardExperiencePoints = rewardExperiencePoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            Gold = gold;
        }
    } 
}
