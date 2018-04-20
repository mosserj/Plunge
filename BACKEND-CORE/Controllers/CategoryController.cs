using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Core.Model;
using backend.Core.Interfaces;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly IEntityFrameworkApplicationRepository _entityRepository;
        public CategoryController(IEntityFrameworkApplicationRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult ret = null;
            List<Category> list = new List<Category>();

            try
            {
                IEnumerable<Category> categories = _entityRepository.GetCategories();
                if (categories.Count() > 0)
                {
                    // NOTE: Declare 'list' outside the using to avoid 
                    // it being disposed before it is returned.
                    list = categories.OrderBy(p => p.CategoryName).ToList();
                    ret = StatusCode(StatusCodes.Status200OK, list);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound,
                                   "Can't Find Categories");
                }
            }
            catch (Exception ex)
            {
                ret = HandleException(ex,
                     "Exception trying to get all Categories");
            }

            return ret;
        }
    }
}
