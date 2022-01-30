/*
 * Kristian Kerrigan
 * Clue.cs
 * This class holds the information for an individual clue
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace KKerrigan_Project1
{ 
    public class Clue : IComparable
    {
        // Properties
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public int? Value { get; set; }
        public DateTime Airdate { get; set; }
        public int Category_Id { get; set; }
        public int? Game_Id { get; set; }
        public object Invalid_Count { get; set; }

        // Methods
        public int CompareTo(object obj)
        {
            Clue clue = (Clue)obj;

            // Compare each value and return 0 if they are the same and 1
            // if not  
            if ((this.Airdate == clue.Airdate) &&
            (this.Answer == clue.Answer) &&
            (this.Category_Id == clue.Category_Id) &&
            (this.Game_Id == clue.Game_Id) &&
            (this.Id == clue.Id) &&
            (this.Invalid_Count == clue.Invalid_Count) &&
            (this.Question == clue.Question) &&
            (this.Value == clue.Value))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override string ToString() =>
            // Return the clue as a serialized JSON object
            JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
