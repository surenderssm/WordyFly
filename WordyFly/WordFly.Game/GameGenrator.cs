using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Common.Exceptions;

using WordFly.Shared.Model;

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
                    game = new GameSession(60, 10, 1, 30);
                    break;
                case GameType.HighAdvanced:
                    game = new GameSession(14, 14, 2, 20);
                    break;
                default:
                    game = new GameSession(10, 10, 2, 30);
                    break;
            }
            //TODO: Surender add probability to the Game artifact
            game.MasterAlpha = Utility.AlphaGenerator.GenerateRawAlpha(game.MaximumRawCharactersRequired, game.VowelsProbability);
            UpdateGameWithSessions(ref game);
            return game;
        }


        private static void UpdateGameWithSessions(ref GameSession game)
        {
            List<GameAtomicState> states = new List<GameAtomicState>();

            int sizeOfSession = game.SizeOfState;
            int sessionJumpCounter = game.SessionJumpCounter;

            GameAtomicState firstState = new GameAtomicState();
            firstState.Id = 0;
            firstState.StartMasterAlphaIndex = 0;
            firstState.Count = sizeOfSession;
            states.Add(firstState);

            int nextStartIndexOfState = firstState.StartMasterAlphaIndex + sessionJumpCounter;

            for (int counter = 1; counter < game.NumberOfStates; counter++)
            {
                GameAtomicState state = new GameAtomicState();
                state.Id = counter;
                state.StartMasterAlphaIndex = nextStartIndexOfState;
                state.Count = sizeOfSession;
                states.Add(state);
                nextStartIndexOfState = state.StartMasterAlphaIndex + sessionJumpCounter;
            }

            // Taking copy as ref object can not be passed in anonymous funciton
            List<AtomicAlpha> masterAlpha = game.MasterAlpha.ToList();
            // FIll the Valid words in Parallel
            Parallel.For(0, states.Count, stateIndex =>
            {
                FillValidWords(states.ElementAt(stateIndex), masterAlpha);
            });
            game.States = new GameState { Items = states };
        }


        // Fill ValidWords
        public static void FillValidWords(GameAtomicState state, List<AtomicAlpha> masterList)
        {
            string inputStream = string.Empty;
            int minimumWordLength = 3;
            try
            {
                for (int index = state.StartMasterAlphaIndex; index <= state.EndMasterAlphaIndex; index++)
                {
                    inputStream += masterList.ElementAt(index).Name;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new InsufficientMasterAlphaException(String.Format("Filling Valid Words StartIndex: {0} EndIndex : {1}. Exception :{2}", state.StartMasterAlphaIndex, state.EndMasterAlphaIndex, ex.ToString()));
            }
            var wordsDictionary = Utility.WordsWiki.GetAllValidWords(inputStream, minimumWordLength);
            state.ValidWords = new List<Word>();

            wordsDictionary.Keys.ToList().ForEach(key =>
            {
                state.ValidWords.Add(new Word(wordsDictionary[key].value));
            });
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
