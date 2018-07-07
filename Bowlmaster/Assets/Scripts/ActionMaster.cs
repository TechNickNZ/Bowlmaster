using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

	public enum Action
    {
        Tidy,
        Reset,
        EndTurn,
        EndGame
    }

    private int bowl = 1;
    private int[] bowls = new int[21];

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10) {throw new UnityException("Pin count out of range");}

        bowls[bowl - 1] = pins;
        
        if (bowl >= 19)  //Last frame special cases
        {
            return LastFrame(pins);
        }

        if (bowl % 2 != 0)  // First Bowl of frame
        {
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else
            {
                bowl++;
                return Action.Tidy;
            }
        }
        else if (bowl % 2 == 0) // Second Bowl of frame
        {
            bowl++;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return!");
    }
    
    private Action LastFrame(int pins)
    {
        if (bowl == 21)
        {
            return Action.EndGame;
        }

        if (pins == 10)
        {
            bowl++;
            return Action.Reset;
        }
        if (bowl == 19)
        {
            bowl++;
            return Action.Tidy;
        }
        if (bowls[18] != 10)
        {
            if((bowls[18] + bowls[19]) == 10)
            {
                bowl++;
                return Action.Reset;
            }
            else
            {
                return Action.EndGame;
            }
        }
        else
        {
            bowl++;
            return Action.Tidy;
        }
    }
}
