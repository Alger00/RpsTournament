using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RpsTournament.Core
{
    public static class GameLogic
    {
        public static Move GetComputerMove()
        { 
            Move[] moves = Enum.GetValues<Move>();
            return moves[Random.Shared.Next(moves.Length)];
        }
        public static RoundResult GetResult(Move player, Move computer)
        {
            if (player == computer) return RoundResult.Viik;
            return (player, computer) switch
            {
                (Move.Kivi, Move.Käärid) => RoundResult.Võit,
                (Move.Paber, Move.Kivi) => RoundResult.Võit,
                (Move.Käärid, Move.Paber) => RoundResult.Võit,
                _ => RoundResult.Kaotus
            };
        }
    }
}
