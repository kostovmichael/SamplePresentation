using System.Collections.Generic;
using SampleWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsConventionController : ControllerBase
    {

        private readonly IContactRepository _contacts;

        public ContactsConventionController(IContactRepository contacts)
        {
            _contacts = contacts;
        }

        // GET api/contactsconvention
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _contacts.GetAll();
        }

        // GET api/contactsconvention/{guid}
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Contact> Get(string id)
        {
            var contact = _contacts.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST api/contactsconvention
        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            _contacts.Add(contact);

            return CreatedAtRoute("GetById", new { id = contact.ID }, contact);
        }

        #region snippet_ApiConventionMethod
        // PUT api/contactsconvention/{guid}
        [HttpPut("{id}")]
        public IActionResult Update(string id, Contact contact)
        {
            var contactToUpdate = _contacts.Get(id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }

            _contacts.Update(contact);

            return NoContent();
        }
        #endregion

        // DELETE api/contactsconvention/{guid}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var contact = _contacts.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contacts.Remove(id);

            return NoContent();
        }
    }
}
