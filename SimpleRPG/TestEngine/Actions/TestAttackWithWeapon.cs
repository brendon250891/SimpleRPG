using Engine.Actions;
using Engine.Factories;
using Engine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine.Actions
{
    [TestClass]
    public class TestAttackWithWeapon
    {
        [TestMethod]
        public void TestConstructorGoodParameters()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            AttackWithWeapon attackWithWeapon = new(pointyStick, 1, 5);

            Assert.IsNotNull(attackWithWeapon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorItemIsNotAWeapon()
        {
            GameItem healthPotion = ItemFactory.CreateGameItem(2001);

            AttackWithWeapon attackWithWeapon = new(healthPotion, 1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorMinimumDamageLessThanZero()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            AttackWithWeapon attackWithWeapon = new(pointyStick, -1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorMaximumDamageLessThanMinimumDamage()
        {
            GameItem pointyStick = ItemFactory.CreateGameItem(1001);

            AttackWithWeapon attackWithWeapon = new(pointyStick, 2, 1);
        }
    }
}
