using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace consoleJKO.Properties.Models
{
    public class CreateListingInput
    {
        public string Username { get; set; }
        public string ItemName { get; set; }
        public string ItemContent { get; set; }
        public int ItemPrice { get; set; }
        public string CategoryName { get; set; }

        public CreateListingInput()
        {

        }
    }

    public class Listing
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string ItemName { get; set; }
        public string ItemContent { get; set; }
        public int ItemPrice { get; set; }
        public DateTime CreateTime { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}",
                ItemName,
                ItemContent,
                ItemPrice,
                CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CategoryName,
                Username
                );
        }


        public static Listing CreateListing(CreateListingInput input)
        {
            Category c = Category.GetCategoryByName(input.CategoryName);
            if ( c == null )
            {
                CreateCategoryInput cInput = new CreateCategoryInput(input.CategoryName);
                c = Category.CreateCategory(cInput);
            }

            Listing item = new Listing()
            {
                Username = input.Username,
                ItemName = input.ItemName,
                ItemContent = input.ItemContent,
                ItemPrice = input.ItemPrice,
                CategoryName = c.Name,
                CreateTime = DateTime.Now,
            };

            Listing dbItem = StorageImpl.CreateListing(item);

            Category.UpdateCount(input.CategoryName, true);

            return dbItem;
        }

        public static bool DeleteListing(int id)
        {

            Listing target = GetListingByID(id);
            if ( target == null )
            {
                return false;
            }

            if ( !StorageImpl.DeleteListingByID(id) )
            {
                return false;
            }

            Category.UpdateCount(target.CategoryName, false);
            return true;
        }


        public static Listing GetListingByID(int id)
        {
            Listing dbItem = StorageImpl.QueryListingByID(id);

            if (dbItem != null)
            {
                return dbItem;
            }

            return null;
        }


        public static List<Listing> GetListingByCategory(string categoryName, string sortBy, bool isAsc)
        {
            List<Listing> lst = StorageImpl.QueryListingByCategory(categoryName);

            IEnumerable<Listing> query;

            if (sortBy == "sort_price" && isAsc)
            {
                query = from item in lst
                        orderby item.ItemPrice
                        select item;
            }
            else if (sortBy == "sort_price" && !isAsc)
            {
                query = from item in lst
                        orderby item.ItemPrice descending
                        select item;
            }

            else if (sortBy == "sort_time" && isAsc)
            {
                query = from item in lst
                        orderby item.CreateTime
                        select item;

            }
            else // if (sortBy == "sort_time" && !isAsc)
            {
                query = from item in lst
                        orderby item.CreateTime descending
                        select item;
            }


            List<Listing> ret = new List<Listing>();
            foreach (Listing l in query)
                ret.Add(l);

            return ret;
        }
    }
}
