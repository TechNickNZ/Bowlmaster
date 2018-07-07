using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OnStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28ReturnsEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04BowlStrikeOnFrame10ReturnsReset()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T05Bowl2OnFrame10ReturnsReset()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        Assert.AreEqual(tidy, actionMaster.Bowl(2));
    }

    [Test]
    public void T06BowlSpareOnFrame10ReturnsReset()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        actionMaster.Bowl(8);
        Assert.AreEqual(reset, actionMaster.Bowl(2));
    }

    [Test]
    public void T07Bowl24OnFrame10ReturnsEndgame()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        actionMaster.Bowl(2);
        Assert.AreEqual(endGame, actionMaster.Bowl(4));
    }

    [Test]
    public void T08BowlX4OnFrame10ReturnsTidy()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        actionMaster.Bowl(10); 
        Assert.AreEqual(tidy, actionMaster.Bowl(4));
    }

    [Test]
    public void T09BowlXX4OnFrame10ReturnsEndgame()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(4));
    }

    [Test]
    public void T10CheckResetAtStrikeInLastFrame()
    {
        int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T11CheckResetAtStrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        actionMaster.Bowl(1);
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T12YouTubeRollsEndInEndGame()
    {
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2};
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T13GameEndsAtBowl20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T14BowlX40nFrame10ReturnsTidy()
    {
        actionMaster.Bowl(10); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(10); //3 
        actionMaster.Bowl(10); //4
        actionMaster.Bowl(10); //5
        actionMaster.Bowl(10); //6 
        actionMaster.Bowl(10); //7
        actionMaster.Bowl(10); //8
        actionMaster.Bowl(10); //9
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T15Bowl0XOnFrame2ReturnsEndturn()
    {
        actionMaster.Bowl(0); //1
        actionMaster.Bowl(10); //2
        actionMaster.Bowl(5); //3
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T16Dondi10thFrameTurkey()
    {
        int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T17ZeroOneGivesEndTurn()
    {
        
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }
}
