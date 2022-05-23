using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.ResourceParameters;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
           [FromQuery] AuthorsResourceParamters authorsResourceParamters)
        {
            var authorsFromRepo =_courseLibraryRepository.GetAuthors(authorsResourceParamters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));            
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto authorForCreation)
        {
            var authorToAdd = _mapper.Map<Author>(authorForCreation);
            _courseLibraryRepository.AddAuthor(authorToAdd);
            _courseLibraryRepository.Save();

            var author = _mapper.Map<AuthorDto>(authorToAdd);
            return CreatedAtRoute("GetAuthor",
                new { authorId = author.Id },
                author);
        }

        [HttpOptions]
        public IActionResult GetAuthorOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }        
    }
}
