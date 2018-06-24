using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public struct PersistentPlayerData : IComparable<PersistentPlayerData>
{
  public readonly int playerId;
  public Color playerColor;
  public string playerName;
  public int numberOfGames;
  public int kills;
  public int wins;
  public int rank;

  public PersistentPlayerData(Color color, string name)
  {
    System.Random random = new System.Random();
    playerId = random.Next();

    playerColor = color;
    playerName = name;
    numberOfGames = 0;
    kills = 0;
    wins = 0;
    rank = 0;
  }

  public override string ToString()
  {
    return "Id: " + playerId + " Name: " + playerName;
  }

  /// <summary>
  /// For reference, a lower number means earlier in a sorted list and a lower (better) rank
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public int CompareTo(PersistentPlayerData other)
  {
    //Judge by wins first
    if(this.wins > other.wins)
    {
      return -1;
    }
    else if (this.wins < other.wins)
    {
      return 1;
    }
    //Oh, same number of wins? How about kills?
    else if(this.kills > other.kills)
    {
      return -1;
    }
    else if(this.kills < other.kills)
    {
      return 1;
    }
    //Shit, ok, number of games played then?!?
    else if (this.numberOfGames > other.numberOfGames)
    {
      return -1;
    }
    else if (this.numberOfGames < other.numberOfGames)
    {
      return 1;
    }
    return 0;
  }

  /// <summary>
  /// Evaluates a list of player data objects and assigns their relative ranking
  /// </summary>
  /// <param name="data"></param>
  public static void SetRanks(List<PersistentPlayerData> data)
  {
    data.Sort();

    int rank = 1;
    PersistentPlayerData previousData = data[0];
    
    for(int i = 0; i < data.Count; ++i)
    {
      PersistentPlayerData currentPlayerData = data[i];
      if (currentPlayerData.CompareTo(previousData) == 0)
      {
        // If they are equal, we assign them the same rank, obviously
        currentPlayerData.rank = rank;
      }
      else
      {
        // This makes sure we get the proper rank numbers. If two people are tied for first, 
        // the third person is third rank, not second
        rank = i + 1;
        currentPlayerData.rank = rank;
      }
      previousData = currentPlayerData;
      data[i] = currentPlayerData;
    }
  }
}