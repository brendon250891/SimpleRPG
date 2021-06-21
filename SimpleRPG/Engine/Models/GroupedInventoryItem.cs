using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GroupedInventoryItem : BaseNotification
    {
        #region Private Properties

        private GameItem _item;

        private int _quantity;

        #endregion

        #region Public Properties

        public GameItem Item
        {
            get { return _item; }
            set
            {
                _item = value;

                OnPropertyChanged(nameof(Item));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;

                OnPropertyChanged(nameof(Quantity));
            }
        }

        #endregion

        public GroupedInventoryItem(GameItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
