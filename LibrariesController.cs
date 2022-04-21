using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using System;
using System.Collections.Generic;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController : ControllerBase
    {
        IBooksService IBooksService;
        ILibrariesService ILibrariesService;
        private int statusCode = 404;

        public LibrariesController(IBooksService iBooksService, ILibrariesService iLibrariesService)
        {
            IBooksService = iBooksService;
            ILibrariesService = iLibrariesService;

        }

        [Route("{libraryId:int}/books")]
        [HttpGet]
        public IActionResult Get(int libraryId)
        {
           // int[] arr_Lib = new int[libraryId];
            var results = ILibrariesService.Get(new int[libraryId]);
            if (results != null && results.Result.Any())
            {
                return Ok(IBooksService.Get(libraryId, null).Result);
            }
            else
            {
                return StatusCode(statusCode);
            }
        }

        [Route("{libraryId:int}/books")]
        [HttpPost]
        public IActionResult PostAsync(Book book)
        {
            if (book != null)
            {
                int[] arr_Lib = new int[book.LibraryId];
                var results = ILibrariesService.Get(arr_Lib);
                if (results != null && results.Result.Any())
                {
                    IBooksService.Add(book);
                    statusCode = 201;
                }
            }
            return StatusCode(statusCode);
        }

        [Route("{libraryId:int}")]
        [HttpDelete]
        public IActionResult Delete(int libraryId)
        {
            int[] arr_Lib = new int[libraryId];
            var results = ILibrariesService.Get(arr_Lib);
            if (results != null && results.Result.Any())
            {
                ILibrariesService.Delete(results.Result.FirstOrDefault());
                statusCode = 204;
            }
            return StatusCode(statusCode);
        }

        [HttpPost]
        public IActionResult PostAsync(Library library)
        {
            try
            {
                if (library != null)
                {
                    ILibrariesService.Add(library);
                   
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode(statusCode);
        }
    }
}
