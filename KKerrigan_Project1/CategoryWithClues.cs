/*
 * Kristian Kerrigan
 * CategoryWithClues.cs
 * This class holds the category information as well as all of the clues in that category.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KKerrigan_Project1
{
    public class CategoryWithClues : IComparable
    {
        // Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int Clues_Count { get; set; }
        public Clue[] Clues { get; set; }

        // Methods
        public int CompareTo(object obj)
        {
            bool differenceFlag = false;
            CategoryWithClues newCategory = (CategoryWithClues)obj;

            if (!this.Id.Equals(newCategory.Id) || !this.Title.Equals(newCategory.Title) || !this.Clues_Count.Equals(newCategory.Clues_Count))
            {
                differenceFlag = true;
            }
            else
            {
                bool cluesDifferent = true;
                for (int i = 0; i < this.Clues_Count; ++i)
                {
                    if (Clues[i].CompareTo(newCategory.Clues[i]) != 0)
                    {
                        cluesDifferent = false;
                        break;
                    }
                }

                if (!cluesDifferent)
                {
                    differenceFlag = true;
                }
            }

            return differenceFlag ? 1 : 0;
        }

        public override string ToString() => Title;
    }
}
