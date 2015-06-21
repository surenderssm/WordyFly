using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Shared.Model
{
    public class GameSession
    {
        private string _logicalGroup = null;
        public string LogicalGroup
        {
            get
            {
                if (String.IsNullOrEmpty(_logicalGroup))
                {
                    _logicalGroup = "Default1";
                }
                return _logicalGroup;
            }
            set { _logicalGroup = value; }
        }
        public Guid ID { get; set; }

        public string Name { get; set; }

        public int CurrentState { get; set; }

        public DateTime StartTime { get; set; }

        public long GameDurationInSeconds { get; set; }


        public DateTime EndTime { get; set; }

        /// <summary>
        /// States present in teh game
        /// </summary>
        public GameState States { get; set; }

        public int NumberOfStates { get; set; }
        // Number of Alpha in a partuclar Session

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
                return SizeOfState * 2 + (SessionJumpCounter * (NumberOfStates - 1));

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
                    throw new Exception("SessionJumpCounter will be always less then SizeOf Session");
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

    public class GameState
    {
        public string GameID { get; set; }

        public List<GameAtomicState> Items;

    }


    public class GameAtomicState
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

        public AtomicAlpha(char charCode)
        {
            Name = charCode.ToString();
            CodeValue = (int)(charCode)-BaseCodeValue;
        }
        public AtomicAlpha()
        {

        }
    }
}

