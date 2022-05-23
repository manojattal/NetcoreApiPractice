using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courses = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}", Name = "GetCourse")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto courseForCreation)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var course = _mapper.Map<Course>(courseForCreation);
            _courseLibraryRepository.AddCourse(authorId, course);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(course);
            return CreatedAtRoute("GetCourse",
                new { authorId = authorId, courseId = courseToReturn.Id },
                courseToReturn
                );
        }

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseFromRepo == null)
            {
                var courseToAdd = _mapper.Map<Course>(course);
                courseToAdd.Id = courseId;
                _courseLibraryRepository.AddCourse(authorId, courseToAdd);
                _courseLibraryRepository.Save();

                var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
                return CreatedAtRoute("GetCourse", new { authorId, courseId = courseToReturn.Id }, courseToReturn);
            }
            _mapper.Map(course, courseFromRepo);
            _courseLibraryRepository.UpdateCourse(courseFromRepo);
            _courseLibraryRepository.Save();
            return NoContent();           
        }

        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdateCourseForAuthor(
            Guid authorId,
            Guid courseId,
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseFromRepo == null)
            {
                var courseForCreation = new CourseForUpdateDto();
                patchDocument.ApplyTo(courseForCreation, ModelState);

                if (!TryValidateModel(courseForCreation))
                {
                    return ValidationProblem(ModelState);
                }

                var course = _mapper.Map<Course>(courseForCreation);
                course.Id = courseId;
                _courseLibraryRepository.AddCourse(authorId, course);
                _courseLibraryRepository.Save();

                var courseToReturn = _mapper.Map<CourseDto>(course);
                return CreatedAtRoute("GetCourse",
                    new { authorId = authorId, courseId = courseToReturn.Id },
                    courseToReturn
                    );
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseFromRepo);
            patchDocument.ApplyTo(courseToPatch, ModelState);
            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(courseToPatch, courseFromRepo);
            _courseLibraryRepository.UpdateCourse(courseFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public ActionResult DelteCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteCourse(courseFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}
