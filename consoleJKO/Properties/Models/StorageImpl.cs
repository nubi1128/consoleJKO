using System;
using System.Collections.Generic;

namespace consoleJKO.Properties.Models
{
    public static class StorageImpl
    {
        // User
        public static Dictionary<string, User> userTable = new Dictionary<string, User>();

        public static User QueryUser(string username)
        {
            if (userTable.ContainsKey(username))
            {
                return userTable[username];
            }
            else
            {
                return null;
            }
        }

        public static User CreateUser(User item)
        {
            StorageImpl.userTable.Add(item.UserName, item);

            return item;
        }

        // Listing
        public static Dictionary<int, Listing> listingTable = new Dictionary<int, Listing>();

        private static int ListingID = 100000;
        private static int getListingID()
        {
            ListingID = ListingID + 1;
            return ListingID;
        }

        public static Listing QueryListingByID(int id)
        {
            if (listingTable.ContainsKey(id))
            {
                return listingTable[id];
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteListingByID(int id)
        {
            if (listingTable.ContainsKey(id))
            {
                listingTable.Remove(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Listing> QueryListingByCategory(string categoryName)
        {
            List<Listing> ret = new List<Listing>();
            foreach( KeyValuePair<int, Listing> kvp in listingTable )
            {
                if ( kvp.Value.CategoryName == categoryName )
                {
                    ret.Add(kvp.Value);
                }
            }
            return ret;
        }

        public static Listing CreateListing(Listing item)
        {
            int c = getListingID();
            item.ID = c;
            StorageImpl.listingTable.Add(c, item);

            return item;
        }

        // Category
        public static Dictionary<string, Category> categoryTable = new Dictionary<string, Category>();

        public static Category QueryCategoryByName(string name)
        {
            if (categoryTable.ContainsKey(name))
            {
                return categoryTable[name];
            }
            else
            {
                return null;
            }
        }

        public static Category CreateCategory(Category item)
        {
            StorageImpl.categoryTable.Add(item.Name, item);

            return item;
        }

        public static List<Category> GetTopCategory()
        {
            int maxCount = 0;
            List<Category> ret = new List<Category>();

            foreach (Category c in StorageImpl.categoryTable.Values)
            {
                if (c.Count > maxCount)
                {
                    maxCount = c.Count;
                }
            }

            foreach (Category c in StorageImpl.categoryTable.Values)
            {
                if (c.Count == maxCount)
                {
                    ret.Add(c);
                }
            }

            return ret;
        }

        public static bool UpdateCategoryCount(string name, bool isAddOne)
        {
            Category c = QueryCategoryByName(name);
            if (c != null)
            {
                if (isAddOne)
                {
                    categoryTable[name].Count += 1;
                }
                else
                {
                    categoryTable[name].Count -= 1;
                }

                return true;
            }
            return false;
        }

        /*
        table ["user"] = userTable

        public static PutItemOutput PutItem(PutItemInput input)
        {
            if (input.tableName == "user")
            {
                StorageClass.userTable.Add(input.key, input.data);
            }
            return input.data;
        }
        */

    }
}
