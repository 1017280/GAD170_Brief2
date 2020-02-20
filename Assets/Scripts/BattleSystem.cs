using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Do Round
/// - Fight Over
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam teamA,teamB; //References to TeamA and TeamB
    public FightManager fightManager; // References to our FightManager.

    public float battlePrepTime = 2;  // the amount of time we need to wait before a battle starts
    public float fightCompletedWaitTime = 2; // the amount of time we need to wait till a fight/round is completed.
    
    /// <summary>
    /// This occurs every round or every X number of seconds, is the core battle logic/game loop.
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRound()
    {
       
        // waits for a float number of seconds....
        yield return new WaitForSeconds(battlePrepTime);

        //checking for no dancers on either team
        if(teamA.allDancers.Count == 0 && teamB.allDancers.Count == 0)
        {
            Debug.LogWarning("DoRound called, but there are no dancers on either team. DanceTeamInit needs to be completed");
            // This will be called if there are 0 dancers on both teams.

        }
        else if (teamA.activeDancers.Count > 0 && teamB.activeDancers.Count > 0)
        {
            int r1 = Random.Range(0, teamA.activeDancers.Count);
            int r2 = Random.Range(0, teamB.activeDancers.Count);

            fightManager.Fight(teamA.activeDancers[r1], teamB.activeDancers[r2]);
        }
        else
        {
            // IF we get to here...then we have a team has won...winner winner chicken dinner.
            DanceTeam winner = null; // null is the same as saying nothing...often seen as a null reference in your logs.
          
            winner = teamA.activeDancers.Count > teamB.activeDancers.Count ? teamA : teamB;

            //Enables the win effects, and logs it out to the console.
            winner.EnableWinEffects();
            BattleLog.Log(winner.ToString(), winner.teamColor);

            Debug.Log("DoRound called, but we have a winner so Game Over");
          
        }
    }

    // This is where we can handle what happens when we win or lose.
    public void FightOver(Character winner, Character defeated, float outcome)
    {
        if (outcome != 0)
        {
            defeated.DealDamage(Mathf.Abs(outcome) * Random.Range(0.1f, 0.4f));
        }
        
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(HandleFightOver());
    }



    /// <summary>
    /// Used to Request A round.
    /// </summary>
    public void RequestRound()
    {
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(DoRound());
    }

    /// <summary>
    /// Handles the end of a fight and waits to start the next round.
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightCompletedWaitTime);
        teamA.DisableWinEffects();
        teamB.DisableWinEffects();
        Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");
        RequestRound();
    }
}
