using MetalTheist.Data.Entities;
using MetalTheist.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> Get()
        {
            try
            {
                var results = await articleRepository.GetAllArticlesAsync();

                return results;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Article>> GetArticleById(int id)
        {
            try
            {
                var result = await articleRepository.GetArticleAsyncById(id);
                if (result == null) return NotFound($"There is no article with id {id}");

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }
    }
}
