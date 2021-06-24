using Engine.Actions;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon
        }

        #region Public Properties

        public ItemCategory Category { get; }

        public int ItemTypeID { get; }

        public string Name { get; }

        public int Price { get; }

        public bool IsUnique { get; }

        public AttackWithWeapon Action { get; set; }

        #endregion

        public GameItem(ItemCategory category, int itemTypeID, string name, int price, bool isUnique = false, AttackWithWeapon action = null)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price, IsUnique, Action);
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
    }
}
