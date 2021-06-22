using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using System.ComponentModel;
using Engine.EventArgs;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotification
    {
        #region Events

        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #endregion

        #region Private Properties

        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;

        #endregion

        #region Public Properties

        public Player CurrentPlayer 
        { 
            get { return _currentPlayer; }
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                }

                _currentPlayer = value;

                if (_currentPlayer != null)
                {
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                }
            }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(CanMoveNorth));
                OnPropertyChanged(nameof(CanMoveWest));
                OnPropertyChanged(nameof(CanMoveEast));
                OnPropertyChanged(nameof(CanMoveSouth));

                CompleteQuestsAtLocation();
                GivePlayerQuests();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.Trader;
            }
        }
        public World CurrentWorld { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
            }
        }
        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }
        public bool CanMoveNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        public bool CanMoveWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        public bool CanMoveEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
        public bool CanMoveSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;

        #endregion

        public GameSession()
        {
            CurrentPlayer = new Player("Brendon", "Fighter", 10, 10, 0, 1, 1000000);

            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        #region Public Methods

        public void Move(string direction)
        {
            switch (direction)
            {
                case "North":
                    if (CanMoveNorth)
                    {
                        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
                    }
                    break;
                case "West":
                    if (CanMoveWest)
                    {
                        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
                    }
                    break;
                case "East":
                    if (CanMoveEast)
                    {
                        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
                    }
                    break;
                case "South":
                    if (CanMoveSouth)
                    {
                        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
                    }
                    break;
                default:
                    break;
            }
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon to attack!");
                return;
            }

            // Determine damage to monster.
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else
            {
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} points.");             
                CurrentMonster.TakeDamage(damageToMonster);
            }

            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"{CurrentMonster.Name} attacks, but misses!");
                }
                else
                {
                    RaiseMessage($"{CurrentMonster.Name} hit you for {damageToPlayer} points.");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }

        #endregion

        #region Private Methods

        private void GivePlayerQuests()
        {
            foreach (Quest quest in CurrentLocation.AvailableQuests)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new(quest));

                    RaiseMessage("");
                    RaiseMessage($"You received the quest: {quest.Name}.");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with:");
                    foreach(ItemQuantity itemQuantity in quest.ItemsNeeded)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} - {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"{quest.RewardExperiencePoints} experience points.");
                    RaiseMessage($"{quest.RewardGold} gold.");
                    foreach (ItemQuantity item in quest.RewardItems)
                    {
                        RaiseMessage($"{item.Quantity} - {ItemFactory.CreateGameItem(item.ItemID).Name}");
                    }
                }
            }
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.AvailableQuests)
            {
                QuestStatus questToComplete = CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllItems(quest.ItemsNeeded))
                    {
                        // Remove the quest items from the players inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsNeeded)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the quest: {quest.Name}");

                        // Give the player their rewards.
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points.");
                        CurrentPlayer.GainExperience(quest.RewardExperiencePoints);

                        RaiseMessage($"You receive {quest.RewardGold} gold.");
                        CurrentPlayer.AddGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            RaiseMessage($"You receive {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }

                        questToComplete.Complete();
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"The {CurrentMonster.Name} killed you.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.GainExperience(CurrentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.AddGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name}");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs e)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }
        #endregion
    }
}
