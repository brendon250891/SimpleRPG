using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        #region Public Properties

        public int ItemTypeID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public bool IsUnique { get; set; }

        #endregion

        public GameItem(int itemTypeID, string name, int price, bool isUnique = false)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price, IsUnique);
        }
    }
}
