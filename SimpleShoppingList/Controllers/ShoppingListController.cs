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
        public static List<ShoppingList> ShoppingLists = new List<ShoppingList>
        {
            new ShoppingList {Id = 0, Name = "Web", Items =
            {
                new Item {Id = 0, Name = "ASP.NET", ShoppingListId = 0},
                new Item {Id = 1, Name = "React", ShoppingListId = 0},
                new Item {Id = 2, Name = "JS", ShoppingListId = 0}
            }},
            new ShoppingList {Id = 1, Name = "Mobile"}
        };

        // GET: api/ShoppingList/5
        public IHttpActionResult Get(int id)
        {
            var result =
                ShoppingLists.FirstOrDefault(s => s.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/ShoppingList
        public IEnumerable Post([FromBody] ShoppingList newList)
        {
            newList.Id = ShoppingLists.Count;
            ShoppingLists.Add(newList);

            return ShoppingLists;
        }
    }
}
