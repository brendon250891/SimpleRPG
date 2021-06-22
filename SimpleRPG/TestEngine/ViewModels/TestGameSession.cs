using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestCreateGameSession()
        {
            GameSession gameSesion = new();

            Assert.IsNotNull(gameSesion.CurrentPlayer);
            Assert.AreEqual("Town Square", gameSesion.CurrentLocation.Name);
        }

        [TestMethod]
        public void TestPlayerMovesHomeAndIsCompletelyHealedOnKilled()
        {
            GameSession gameSession = new();

            gameSession.CurrentPlayer.TakeDamage(999);

            Assert.AreEqual("Home", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.MaximumHitPoints, gameSession.CurrentPlayer.CurrentHitPoints);
        }
    }
}
