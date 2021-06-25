using MetalTheist.Data.Entities;
using MetalTheist.Data.Extensions;
using MetalTheist.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly LinkGenerator linkGenerator;

        public ArticlesController(IArticleRepository articleRepository, LinkGenerator linkGenerator)
        {
            this.articleRepository = articleRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> Get(bool includeStatistics=false)
        {
            try
            {
                var results = await articleRepository.GetAllArticlesAsync(includeStatistics);

                return results.OrderBy(r => r.Id).ToList();
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

        [HttpGet("{moniker}")]
        public async Task<ActionResult<Article>> GetArticleByMoniker(string moniker)
        {
            try
            {
                var result = await articleRepository.GetArticleAsyncByMoniker(moniker);
                if (result == null) return NotFound($"There is no article with moniker {moniker}");

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Post(Article article)
        {
            try
            {
                var existing = await articleRepository.GetArticleAsyncByMoniker(article.Moniker);

                if(existing != null)
                {
                    return BadRequest($"There is already an article with moniker {article.Moniker}");
                }

                var location = linkGenerator.GetPathByAction("Get", "Articles", new { moniker = article.Moniker });

                if(string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current moniker!");
                }

                articleRepository.Add(article);

                if(await articleRepository.CommitAsync())
                {
                    return Created("", article);
                }
                else
                {
                    return BadRequest("Failed to create new article");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<Article>> Put(string moniker, Article article)
        {
            try
            {
                var oldArticle = await articleRepository.GetArticleAsyncByMoniker(article.Moniker);
                if (oldArticle == null) return NotFound($"Could not find article with moniker of {moniker}");

                oldArticle.Map(article);

                if (await articleRepository.CommitAsync())
                {
                    return oldArticle;
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Article>> Put(int id, Article article)
        {
            try
            {
                var oldArticle = await articleRepository.GetArticleAsyncById(article.Id);
                if (oldArticle == null) return NotFound($"Could not find article with id of {id}");

                oldArticle.Map(article);

                if (await articleRepository.CommitAsync())
                {
                    return oldArticle;
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var oldArticle = await articleRepository.GetArticleAsyncByMoniker(moniker);
                if(oldArticle == null) return NotFound($"Could not find article with moniker of {moniker}");

                articleRepository.Delete(oldArticle);

                if(await articleRepository.CommitAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to delete the article!");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldArticle = await articleRepository.GetArticleAsyncById(id);
                if (oldArticle == null) return NotFound($"Could not find article with id of {id}");

                articleRepository.Delete(oldArticle);

                if (await articleRepository.CommitAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to delete the article!");
        }
    }
}
