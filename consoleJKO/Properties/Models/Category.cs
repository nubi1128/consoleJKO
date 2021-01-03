using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace consoleJKO.Properties.Models
{
    public class CreateCategoryInput
    {
        public string Name { get; set; }

        public CreateCategoryInput(string inputName)
        {
            Name = inputName;
        }
    }

    public class Category
    {
        public int Count;
        public string Name { get; set; }


        public static Category CreateCategory(CreateCategoryInput input)
        {
            Category item = new Category()
            {
                Count = 0,
                Name = input.Name
            };

            return StorageImpl.CreateCategory(item);
        }

        public static Category GetCategoryByName(string name)
        {
            Category dbItem = StorageImpl.QueryCategoryByName(name);

            if (dbItem != null)
            {
                return dbItem;
            }

            return null;
        }

        public static List<Category> GetTopCategory()
        {
            return StorageImpl.GetTopCategory();
        }

        public static bool UpdateCount(string name, bool isAddOne)
        {
            return StorageImpl.UpdateCategoryCount(name, isAddOne);
        }
    }
}
