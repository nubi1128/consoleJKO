# Console JKO App

### A CLI application with Net Core

  It's a console app developed with Net Core which describes a marketplace platform that allows users to do some operation.
  The following are three types of object in the marketplace.
  
  1. `User`
  
      A `User` is a registed user in the market. 
      `User` has its name (the name's alphebet is case insensitive in the market) and its selling items (`Listing`).
      
  2. `Listing`
  
      A `Listing` is a selling item.
      `Listing` has its title, description, price, creating time, category (`Category`) and owner (`User`).
      
  3. `Category`
  
      A `Category` is used for item clasification.
      `Category` has its name and total counts of `Listing` which belongs to the `Category`.
  
### Commands (user allowed to perform) and Outputs :

  1. `REGISTER <username>`
      
      To sign up a user, `username`.
  
      Command output                    | Note  
      ----------------------------      |:-----
      Success                           | registered successfully 
      Error - user already existing     | register failed for user existing 
      
      
        
  2. `CREATE_LISTING <username> <title> <description> <price> <category>` 
      
      To create a item with its `title`, `description`, `price`, and `category`.
      
      The field `username` is for authentication.
    
      
      Command output                    | Note  
      ----------------------------      |:-----
      `listing id`                      | created item successfully, `listing id` is the returned id of the item
      Error - unknown user              | failed to create item for `username` provided is invalid
        
        
  3. `DELETE_LISTING <username> <listing_id>`
  
      To delete the target item on my marketplace with its id, `listing_id`.
      
      The field `username` is for authentication.
      
       Command output                    | Note  
      ----------------------------      |:-----
      Success                           | deleted the item successfully 
      Error - listing does not exist    | failed to delete the item for the item not existing
      Error - listing owner mismatch    | failed to delete the item for the provided username is not the owner of the target item
      Error - unknown user              | failed to delete item for `username` provided is invalid
  
  4. `GET_LISTING <username> <listing_id>`
  
      To get the details of the item by providing its `listing_id`.
      
      The field `username` is for authentication.
  
       Command output                                                                           | Note  
      ------------------------------------------------------------------------------------      |:-----
      `title`\|`description`\|`price`\|`create_at`\|`category`\|`username`                      | returned the target item's details
      Error - not found                                                                         | failed to get the details for the item not existing
      Error - unknown user                                                                      | failed to delete item for `username` provided is invalid
  
  5. `GET_CATEGORY <username> <category>`
      
      To list all items and its details the specific `category`.
      
      The field `username` is for authentication.
      
       Command output                                                                           | Note  
      ------------------------------------------------------------------------------------      |:-----
      `title`\|`description`\|`price`\|`create_at`\|`category`\|`username`                      | returned all target item's details
      Error - category not found                                                                | failed to get items details for the category not existing
      Error - unknown user                      | failed to delete item for `username` provided is invalid
  
  6. `GET_TOP_CATEGORY <username>`
  
      To list the `category` which has the most items.
      
      The field `username` is for authentication.
      
       Command output                                                                           | Note  
      ------------------------------------------------------------------------------------      |:-----
      `category`                  | returned all category's name 
      Error - unknown user                      | failed to delete item for `username` provided is invalid
      

    
