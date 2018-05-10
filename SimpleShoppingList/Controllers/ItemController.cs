using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleShoppingList.Models;

namespace SimpleShoppingList.Controllers
{
    public class ItemController : ApiController
    {
        // POST: api/Item
        public IHttpActionResult Post([FromBody] Item item)
        {
            var shoppingList =
                ShoppingListController.ShoppingLists
                    .FirstOrDefault(s => s.Id == item.ShoppingListId);

            if (shoppingList == null)
            {
                return NotFound();
            }

            item.Id = shoppingList.Items.Max(i => i.Id) + 1;
            shoppingList.Items.Add(item);

            return Ok(shoppingList);
        }

        // PUT: api/Item/5
        public IHttpActionResult Put(int id, [FromBody] Item item)
        {
            var shoppingList =
                ShoppingListController.ShoppingLists
                    .FirstOrDefault(s => s.Id == item.ShoppingListId);

            if (shoppingList == null)
            {
                return NotFound();
            }

            var changedItem = shoppingList.Items.FirstOrDefault(i => i.Id == id);

            if (changedItem == null)
            {
                return NotFound();
            }

            changedItem.Checked = item.Checked;

            return Ok(shoppingList);
        }

        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            var shoppingList = ShoppingListController.ShoppingLists[0];

            var item = shoppingList.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            shoppingList.Items.Remove(item);

            return Ok(shoppingList);
        }
    }
}
