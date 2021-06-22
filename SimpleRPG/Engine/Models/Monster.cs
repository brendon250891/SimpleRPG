namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        #region Public Properties

        public string ImageName { get; }

        public int MinimumDamage { get; }

        public int MaximumDamage { get; }

        public int RewardExperiencePoints { get; }

        #endregion

        public Monster(string name, string imageName, int maximumHitPoints, int currentHitPoints, int minimumDamage, int maximumDamage, int rewardExperiencePoints, int gold) 
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}.png";
            RewardExperiencePoints = rewardExperiencePoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;            
        }
    } 
}
