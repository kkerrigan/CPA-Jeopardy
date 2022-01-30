/*
 * Kristian Kerrigan
 * Category.cs
 * This class holds all of the information for an individual category.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KKerrigan_Project1
{
    public class Category
    {
        // Member variables
        private int id_;
        private string title_;
        private int clues_count_;

        // Properties
        public int Id
        {
            get { return id_; }
            set { id_ = value; }
        }
        public string Title
        {
            get { return title_; }
            set { title_ = value; }
        }
        public int Clues_Count
        {
            get { return clues_count_; }
            set { clues_count_ = value; }
        }

        // Methods
        public override string ToString() => Title;

    }
}
