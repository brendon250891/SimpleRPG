using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class QuestStatus : BaseNotification
    {
        #region Private Properties

        private bool _isCompleted;

        #endregion

        #region Public Properties

        public Quest PlayerQuest { get; }
        public bool IsCompleted 
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;

                OnPropertyChanged();
            }
        }

        #endregion

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }

        #region Public Methods

        public void Complete()
        {
            IsCompleted = true;
        }

        #endregion
    }
}
