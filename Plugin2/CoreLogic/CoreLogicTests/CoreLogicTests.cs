using System;
using CoreLogic;
using Moq;
using Xunit;

namespace CoreLogicTests
{
    public class CoreLogicTests
    {
        private ShellGameLogic CreateCoreLogic()
        {
            Mock<LMRRandom> rand = new Mock<LMRRandom>();
            rand.Setup(r => r.Next(0, 3)).Returns(2);
            return new ShellGameLogic(rand.Object, 3,3);
        }
        [Fact]
        public void GivenPlayer_SelectsCorrectItemOnFirstTry_CheckFOrItemShouldReturnTrue()
        {
            //Arrange
            ShellGameLogic sut = CreateCoreLogic();

            //Act
            bool result = sut.CheckForItem(2);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void GivenPlayer_SelectsCorrectItemOnFirstTry_CheckFOrItemShouldReturnFalse()
        {
            //Arrange
            ShellGameLogic sut = CreateCoreLogic();

            //Act
            bool result = sut.CheckForItem(1);

            //Assert
            Assert.False(result);
        }
        [Fact]
        public void GivenPlayer_SelectsCorrectItemOnFirstTryAndSecondTry_CheckFOrItemShouldReturnFalse()
        {
            //Arrange
            ShellGameLogic sut = CreateCoreLogic();

            //Act
            bool result = sut.CheckForItem(1);
            if(!result)
            {
                result = sut.CheckForItem(0);
            }
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void GivenPlayer_LosesRound_ShouldHaveAStrikeCountAndItShouldBeOne()
        {
            ShellGameLogic sut = CreateCoreLogic();

            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);

            Assert.Equal(1, sut.Strikes);
        }
        [Fact]
        public void GivenPlayer_LosesRound_ShouldHaveAStrikeCountEvenIfReselectingPreviouslySelectedItem()
        {
            ShellGameLogic sut = CreateCoreLogic();
            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(0);
            sut.CheckForItem(2);

            Assert.Equal(1, sut.Strikes);
        }
        [Fact]
        public void GivenPlayer_StrikesOutThenWInsOnSecondTryAndStrikesOutAgain_ThenTheStrikeCountShouldBeTwo()
        {
            ShellGameLogic sut = CreateCoreLogic();

            //Strike 1
            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            sut.ResetItem();

            //Right on the Second Try
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            sut.ResetItem();

            //Strike 2
          
            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            sut.ResetItem();

            Assert.Equal(2, sut.Strikes);
        }
        [Fact]
        public void GivenPlayer_StrikesOut_ThenTheGameShouldBeOVer()
        {
            bool called = false;
            var sut = CreateCoreLogic();
            sut.GameOver += (object sender, EventArgs e) => called = true;
           // ShellGameLogic sut = CreateCoreLogic();

            //Strike 1
            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            sut.ResetItem();

            //Strike 2
            sut.CheckForItem(1);
            sut.CheckForItem(0);
            sut.CheckForItem(2);
            sut.ResetItem();

            //Strike 3

            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            sut.ResetItem();

            Assert.True(called);
        }
        [Fact]
        public void GivenPlayer_FindsAMatch_ShouldRaiseMatchFoundEventWithId()
        {
            bool idSet = false;
            ShellGameLogic sut = CreateCoreLogic();
            sut.MatchMade += (object sender, MatchEventArgs e) => idSet = e.Id >=0;

            sut.CheckForItem(2);
            Assert.True(idSet);

        }
        [Fact]
        public void GivenPlayer_DoesntFindAMatch_ShouldRaiseMatchNotFoundWithIsStrikeFalse()
        {
            bool isStrike = false;
            ShellGameLogic sut = CreateCoreLogic();
            sut.MatchNotMade += (object sender, NoMatchEventArgs e) =>  isStrike = e.IsStrike;

            sut.CheckForItem(0);
            Assert.False(isStrike);

        }
        [Fact]
        public void GivenPlayer_StrikesOut_ShouldRaiseMatchNotFoundWithIsStrikeTrue()
        {
            bool isStrike = false;
            ShellGameLogic sut = CreateCoreLogic();
            sut.MatchNotMade += (object sender, NoMatchEventArgs e) => isStrike = e.IsStrike;

            sut.CheckForItem(0);
            sut.CheckForItem(1);
            sut.CheckForItem(2);
            Assert.True(isStrike);

        }
        [Fact]
        public void GivenPlayer_ChecksAnItem_StartTurnEventSHouldBeRaised()
        {
            bool called = false;
            ShellGameLogic sut = CreateCoreLogic();
            sut.StartTurn += (object sender, EventArgs e) => called = true;

            sut.CheckForItem(2);
         
            Assert.True(called);

        }

        [Fact]
        public void GivenPlayer_SelectsItem_ShouldRaiseCheckingItemEvent()
        {
            bool called = false;
            var sut = CreateCoreLogic();
            sut.CheckingItem += (object sender, ItemEventArgs e) => called = e.Id >= 0;

            sut.CheckForItem(1);

            Assert.True(called);

        }
        [Fact]
        public void GivenPlayer_SelectsRightItem_ShouldRaiseCheckingItemEvent()
        {
            bool called = false;
            var sut = CreateCoreLogic();
            sut.CheckingItem += (object sender, ItemEventArgs e) => called = e.Id >= 0;

            sut.CheckForItem(2);

            Assert.True(called);

        }
        [Fact]
        public void GivenResetItem_Iscalled_ThenItemmResetEventSHouldBeRaised()
        {
            bool called = false;
            var sut = CreateCoreLogic();
            sut.ItemReset += (object sender, EventArgs e) => called = true;

            sut.ResetItem();

            Assert.True(called);

        }
        public void GivenResetItem_Iscalled_ThenResetCompletetEventSHouldBeRaised()
        {
            bool called = false;
            var sut = CreateCoreLogic();
            sut.ResetComplete += (object sender, EventArgs e) => called = true;

            sut.ResetItem();

            Assert.True(called);

        }
    }
}
