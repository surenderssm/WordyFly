
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WordFly.Game.Model
{
   public  class GameSession
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public long CurrentState { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        List<SessionState> States { get; set; }

        public GameSession()
        {
            ID = Guid.NewGuid().ToString();

        }
    }

    public class SessionState
    {
        public int StateID { get; set; }
        public List<AtomicAlpha> ValidAlpha { get; set; }
        public List<Word> ValidWords { get; set; }
    }

    /// <summary>
    /// Valid WOrd
    /// </summary>
    public class Word
    {
        public string Value { get; set; }
        public string Meaning { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
    public class AtomicAlpha
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        // a..z->1..26
        public int CodeValue { get; set; }

        public AtomicAlpha(int value)
        {
            CodeValue = value;
            Name = ((char)value).ToString();
        }
    }
}
