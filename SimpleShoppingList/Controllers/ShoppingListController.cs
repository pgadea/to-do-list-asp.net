using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleShoppingList.Models;

namespace SimpleShoppingList.Controllers
{
    public class ShoppingListController : ApiController
    {
        public static List<ShoppingList> shoppingLists = new List<ShoppingList>
        {
            new ShoppingList {Id = 0, Name = "Web", Items =
            {
                new Item {Name = "ASP.NET"},
                new Item {Name = "React"},
                new Item {Name = "JS"}
            }},
            new ShoppingList {Id = 0, Name = "Mobile"}
        };

        // GET: api/ShoppingList/5
        public IHttpActionResult Get(int id)
        {
            var result =
                shoppingLists.FirstOrDefault(s => s.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/ShoppingList
        public IEnumerable Post([FromBody] ShoppingList newList)
        {
            newList.Id = shoppingLists.Count;
            shoppingLists.Add(newList);

            return shoppingLists;
        }
    }
}
