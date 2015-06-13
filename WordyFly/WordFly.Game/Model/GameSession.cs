
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WordFly.Common;
using WordFly.Common.Exceptions;

namespace WordFly.Game.Model
{

    [DataContract]
    public class GameSession
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int CurrentState { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
            [DataMember]
        public long GameDurationInSeconds { get; set; }
        
        [DataMember]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// States present in teh game
        /// </summary>
        [DataMember]
        public List<GameState> States { get; set; }
        [DataMember]
        public int NumberOfStates { get; set; }
        // Number of Alpha in a partuclar Session
        [DataMember]
        public int SizeOfState { get; set; }

        /// <summary>
        /// Get the Minimum Raw Charactes Required
        /// </summary>
        public int MaximumRawCharactersRequired
        {
            get
            {

                // TODO:Surender revisit
                // SessionJumpCounter will be always less then SizeOf Session

                return SizeOfState * NumberOfStates;

            }
        }

        private int sessionJumpCounter;
        // Number of Jumps to move the block of Session
        public int SessionJumpCounter
        {
            get { return sessionJumpCounter; }
            set
            {
                if (value > SizeOfState)
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
        [DataMember]
        public List<AtomicAlpha> MasterAlpha { get; set; }
        public GameSession()
        {

        }

        /// <summary>
        /// Init the Object
        /// </summary>
        /// <param name="numberOfSessions">Number of Expected session in Game</param>
        /// <param name="sizeOfSession">Number of ALpha in a single Session</param>
        /// <param name="sessionJumpCounter">Number of jumps to change the session</param>
        public GameSession(int numberOfSessions, int sizeOfSession, int sessionJumpCounter)
        {
            ID = Guid.NewGuid();
            this.NumberOfStates = numberOfSessions;
            this.SizeOfState = sizeOfSession;
            this.SessionJumpCounter = sessionJumpCounter;
        }
    }

    [DataContract]
    public class GameState
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        // Start Index in MasterAlphaStream to get the Alpha of Session
        public int StartMasterAlphaIndex { get; set; }
        [DataMember]
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
        [DataMember]
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

    [DataContract]
    /// <summary>
    /// Valid WOrd
    /// </summary>
    public class Word
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Meaning { get; set; }
        [DataMember]
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
    [DataContract]
    public class AtomicAlpha
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DisplayName { get; set; }

        private const int BaseCodeValue = 65; // ASCII('A') :: 65
        [DataMember]
        // [A..Z]:[0..25]
        public int CodeValue { get; set; }

        public AtomicAlpha(int value)
        {
            CodeValue = value;
            Name = ((char)(value + BaseCodeValue)).ToString();
        }
    }
}
