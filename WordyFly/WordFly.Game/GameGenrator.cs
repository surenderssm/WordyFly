using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game.Model;

namespace WordFly.Game
{
    // Handles all the sceanrios/FUnction to create Game
   public class GameGenrator
    {
      
        public GameSession CreateNewGame(GameType gameType)
        {
            //TODO:surender pull game info from different types of game
            GameSession game;
            switch (gameType)
            {
                case GameType.Basic:
                    game = new GameSession(8, 10, 2);
                    break;
                case GameType.Advanced:
                    game = new GameSession(12, 12, 2);
                    break;
                case GameType.Normal:
                    game = new GameSession(10, 10, 2);
                    break;
                case GameType.HighAdvanced:
                    game = new GameSession(14, 14, 2);
                    break;
                default:
                    game = new GameSession(10, 10, 2);
                    break;
            }

            game.MasterAlpha = GenerateRawAlpha(game.MaximumRawCharactersRequired);
            UpdateGameWithSessions(ref game);
            return game;
        }


        private static void UpdateGameWithSessions(ref GameSession game)
        {
            List<GameState> states = new List<GameState>();

            int sizeOfSession = game.SizeOfState;
            int sessionJumpCounter = game.SessionJumpCounter;

            GameState firstState = new GameState();
            firstState.Id = 0;
            firstState.StartMasterAlphaIndex = 0;
            firstState.Count = sizeOfSession;
            states.Add(firstState);

            int nextStartIndexOfState = firstState.EndMasterAlphaIndex + 1;

            for (int counter = 1; counter < game.NumberOfStates; counter++)
            {
                GameState state = new GameState();
                state.Id = counter;
                state.StartMasterAlphaIndex = nextStartIndexOfState;
                state.Count = sizeOfSession;
                states.Add(state);
                nextStartIndexOfState = state.EndMasterAlphaIndex + 1;
            }

            // Taking copy as ref object can not be passed in anonymous funciton
            List<AtomicAlpha> masterAlpha = game.MasterAlpha.ToList();
            // FIll the Valid words in Parallel
            Parallel.For(0, states.Count, stateIndex =>
            {
                states.ElementAt(stateIndex).FillValidWords(masterAlpha);
            });
            game.States = states;
        }

        /// <summary>
        /// Get the Number of Alphabet required given the number of Session
        /// </summary>
        /// <param name="sessionCount"></param>
        /// <returns></returns>
        private static int GetAlphaCount(int sessionCount)
        {
            //TODO:Surender placeholder for the Number of alpha logic
            return 12 * sessionCount;
        }

        /// <summary>
        /// Return the expected number of Randommly genrated Alphas
        /// </summary>
        /// <param name="totalAlpha"></param>
        /// <returns></returns>
        private static List<AtomicAlpha> GenerateRawAlpha(int totalAlpha)
        {
            // Unordered Thread-safe Collection for Alphas
            ConcurrentBag<AtomicAlpha> alphaBag = new ConcurrentBag<AtomicAlpha>();
            Random randomGenerator = new Random();

            Parallel.For(0, totalAlpha, index =>
              {
                  // creates a Alpha with Codevalue between 0 and 26 [A..Z]:[0..25]
                  AtomicAlpha alpha = new AtomicAlpha(randomGenerator.Next(0, 26));
                  alphaBag.Add(alpha);
              });

            //TODO: probability of Vowels
            // TODO: probability of % of vowels
            
            return alphaBag.ToList();
        }

    }
}
