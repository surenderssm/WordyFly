
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordFly.Common;
using WordFly.Common.Exceptions;

namespace WordFly.Game.Model
{
    public class GameSession
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public long CurrentState { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// States present in teh game
        /// </summary>
        public List<SessionState> States { get; set; }

        public int NumberOfSessions { get; set; }
        // Number of Alpha in a partuclar Session
        public int SizeOfSession { get; set; }

        /// <summary>
        /// Get the Minimum Raw Charactes Required
        /// </summary>
        public int MaximumRawCharactersRequired
        {
            get
            {

                // TODO:Surender revisit
                // SessionJumpCounter will be always less then SizeOf Session

                return SizeOfSession * NumberOfSessions;

            }
        }

        private int sessionJumpCounter;
        // Number of Jumps to move the block of Session
        public int SessionJumpCounter
        {
            get { return sessionJumpCounter; }
            set
            {
                if (value > SizeOfSession)
                {
                    throw new GenericGameException("SessionJumpCounter will be always less then SizeOf Session");
                }
                else
                {
                    sessionJumpCounter = value;
                }
            }
        }

        // Container for Raw Alpha Stream
        public List<AtomicAlpha> MasterAlpha { get; set; }

        public GameSession()
        {
            ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Init the Object
        /// </summary>
        /// <param name="numberOfSessions">Number of Expected session in Game</param>
        /// <param name="sizeOfSession">Number of ALpha in a single Session</param>
        /// <param name="sessionJumpCounter">Number of jumps to change the session</param>
        public GameSession(int numberOfSessions, int sizeOfSession, int sessionJumpCounter)
        {
            ID = Guid.NewGuid().ToString();
            this.NumberOfSessions = numberOfSessions;
            this.SizeOfSession = sizeOfSession;
            this.SessionJumpCounter = sessionJumpCounter;
        }
    }

    public class SessionState
    {
        public int Id { get; set; }

        // Start Index in MasterAlphaStream to get the Alpha of Session
        public int StartMasterAlphaIndex { get; set; }

        // Total Number of ALphas of Session
        public int Count { get; set; }

        // Start Index in MasterAlphaStream to get the Alpha of Session
        public int EndMasterAlphaIndex
        {
            get
            {
                return StartMasterAlphaIndex + Count - 1;
            }
        }

        /// <summary>
        /// Contains all the eligible Words which can be in current Session
        /// </summary>
        public List<Word> ValidWords { get; set; }


        // Fill ValidWords
        public void FillValidWords(List<AtomicAlpha> masterList)
        {
            string inputStream = string.Empty;
            int minimumWordLength = 3;
            try
            {
                for (int index = this.StartMasterAlphaIndex; index <= this.EndMasterAlphaIndex; index++)
                {
                    inputStream += masterList.ElementAt(index).Name;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new InsufficientMasterAlphaException(String.Format("Filling Valid Words StartIndex: {0} EndIndex : {1}. Exception :{2}", StartMasterAlphaIndex, EndMasterAlphaIndex, ex.ToString()));
            }
            var wordsDictionary = Utility.WordsWiki.GetAllValidWords(inputStream, minimumWordLength);

            ValidWords = new List<Word>();

            wordsDictionary.Keys.ToList().ForEach(key =>
            {
                ValidWords.Add(new Word(wordsDictionary[key].value));
            });
        }
    }

    /// <summary>
    /// Valid WOrd
    /// </summary>
    public class Word
    {
        public string Value { get; set; }
        public string Meaning { get; set; }

        // Score which will be added TODO: COme up with ENUM for Score or type of Score
        public int Score { get; set; }

        public override string ToString()
        {
            return Value;
        }
        public Word(string value)
        {
            this.Value = value;
        }
    }
    public class AtomicAlpha
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        private const int BaseCodeValue = 65; // ASCII('A') :: 65

        // [A..Z]:[0..25]
        public int CodeValue { get; set; }

        public AtomicAlpha(int value)
        {
            CodeValue = value;
            Name = ((char)(value + BaseCodeValue)).ToString();
        }
    }
}
