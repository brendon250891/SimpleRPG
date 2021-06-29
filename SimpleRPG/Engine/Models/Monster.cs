namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        #region Public Properties

        public string ImageName { get; }

        public int RewardExperiencePoints { get; }

        #endregion

        public Monster(string name, string imageName, int maximumHitPoints, int currentHitPoints, int rewardExperiencePoints, int gold) 
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}.png";
            RewardExperiencePoints = rewardExperiencePoints;      
        }
    } 
}
