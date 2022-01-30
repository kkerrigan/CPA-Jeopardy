/*
 * Kristian Kerrigan
 * Service.cs
 * This class performs all the API calls and file saving for the application
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KKerrigan_Project1
{
    public static class Service
    {
        // Member Variables
        private const string URL = "http://jservice.io/api/";
        private const int MAX_OFFSET = 18500;
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "savedCategories.json";
        private static List<CategoryWithClues> savedCategories = new List<CategoryWithClues>();

        private static Random rng = new Random();

        // Methods
        public static List<CategoryWithClues> GetRandomFilledCategories()
        {
            List<CategoryWithClues> filledCategories = new List<CategoryWithClues>();

            bool updateSavedCategories = false;
            bool categorySaved;
            foreach(Category category in GetRandomCategories())
            {
                CategoryWithClues question = GetCluesForCategory(category.Id);

                // Check if the category is in the saved file and update if needed
                categorySaved = savedCategories.FindAll(c => c.CompareTo(question) == 0).Count() == 1;
                if (!categorySaved)
                {
                    updateSavedCategories = true;

                    savedCategories.Remove(savedCategories.Find(cat => cat.Id == question.Id));
                    savedCategories.Add(question);
                }

                filledCategories.Add(question);
            }

            // Check if the flag flipped and update the save file if needed
            if (updateSavedCategories)
            {
                WriteToFile();
            }

            return filledCategories;
        }

        public static void LoadSavedCategories()
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                string clues = reader.ReadToEnd();
                reader.Close();
                // Deserialize the save file to the C# class
                savedCategories = JsonConvert.DeserializeObject<List<CategoryWithClues>>(clues);
            }
            catch(Exception ex)
            {
                savedCategories = new List<CategoryWithClues>();
            }
        }

        private static List<Category> GetRandomCategories()
        {
            List<Category> categories = new List<Category>();

            for(int i = 0; i < 6; ++i)
            {
                Category category = GetRandomCategory();
                categories.Add(category);
            }
            return categories;
        }

        static private Category GetRandomCategory()
        {
            int offset = rng.Next(0, MAX_OFFSET);

            string json = GetJSONString("categories?count=1&offset=" + offset);

            return JsonConvert.DeserializeObject<List<Category>>(json).First();
        }

        private static CategoryWithClues GetCluesForCategory(int categoryId)
        {
            string json = GetJSONString("category?id=" + categoryId);
            return JsonConvert.DeserializeObject<CategoryWithClues>(json);
        }

        private static string GetJSONString(string url)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString(URL + url);
            client.Dispose();
            return json;
        }

        private static void WriteToFile()
        {
            StreamWriter output = new StreamWriter(path);
            output.WriteLine(JsonConvert.SerializeObject(savedCategories, Formatting.Indented));
            output.Close();
        }

    }
}
