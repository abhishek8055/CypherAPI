using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CypherApi.Controllers
{
    //[Route("api/todo/{action}/{id}")]
    public class TodoController : ApiController
    {
        private readonly CypherDBEntities _context;
        public TodoController()
        {
            _context = new CypherDBEntities();
        }

        // GET: api/Todo
        [HttpGet]
        public IHttpActionResult GetTodoItems()
        {
            try
            {
                var todoList = _context.TodoLists.ToList();
                return Ok(todoList);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // GET: api/Todo/5
        //[HttpGet("{id}")]
        [HttpGet]
        public IHttpActionResult GetTodoItem(int id)
        {
            try
            {
                var todoItem = _context.TodoLists.SingleOrDefault(c => c.Id == id);
                if (todoItem == null)
                {
                    return NotFound();
                }
                return Ok(todoItem);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // POST: api/Todo
        [HttpPost]
        public IHttpActionResult PostTodoItem(TodoList item)
        {
            try
            {
                _context.TodoLists.Add(item);
                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri + "/" + item.Id), item);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // PUT: api/Todo/5
        //[HttpPut("{id}")]
        [HttpPost]
        public IHttpActionResult PutTodoItem(int id, TodoList item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            try
            {
                var todoItemInDB = _context.TodoLists.SingleOrDefault(c => c.Id == id);
                if (todoItemInDB == null)
                {
                    return NotFound();
                }
                todoItemInDB.Name = item.Name;
                todoItemInDB.IsComplete = item.IsComplete;
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // DELETE: api/Todo/5
        //[HttpDelete("{id}")]
        [HttpPost]
        public IHttpActionResult DeleteTodoItem(TodoList item)
        {
            try
            {
                var todoItem = _context.TodoLists.SingleOrDefault(c => c.Id == item.Id);

                if (todoItem == null)
                {
                    return NotFound();
                }
                _context.TodoLists.Remove(todoItem);
                _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}