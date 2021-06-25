using MetalTheist.Core;
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
    [ApiController]
    [Route("api/articles/{id:int}/statistics")]
    public class ArticleStatisticsByIdController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;
        private readonly LinkGenerator linkGenerator;

        public ArticleStatisticsByIdController(IArticleRepository articleRepository, LinkGenerator linkGenerator)
        {
            this.articleRepository = articleRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ArticleStatistic>> Get(int id)
        {
            try
            {
                var articleStatistics = await articleRepository.GetArticleStatisticAsync(id);
                return articleStatistics;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ArticleStatistic>> Post(int id, ArticleStatistic model)
        {
            try
            {
                var article = await articleRepository.GetArticleAsyncById(id);
                if (article == null) return BadRequest("Article does not exist");

                var articleStatistic = article.Statistics;
                if (articleStatistic != null) return BadRequest("This article already has statistics");

                //articleStatistic.Map(model);
                articleStatistic = model;

                article.Statistics = articleStatistic;
                articleRepository.Add(articleStatistic);

                if (await articleRepository.CommitAsync())
                {
                    var url = linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { id }
                        );

                    return Created(url, articleStatistic);
                }
                else
                {
                    return BadRequest("Failed to create new statistic!");
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ArticleStatistic>> Put(int id, ArticleStatistic model)
        {
            try
            {
                var oldArticle = await articleRepository.GetArticleAsyncById(id, includeStatistics: true);
                if (oldArticle == null) return NotFound($"There is no article with moniker {id}");

                if (oldArticle.Statistics == null) return NotFound("There are no statistics for this article");

                oldArticle.Statistics = model;

                if (await articleRepository.CommitAsync())
                {
                    return oldArticle.Statistics;
                }
                else
                {
                    return BadRequest("Could not update this article!");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var statistic = await articleRepository.GetArticleStatisticAsync(id);
                if (statistic == null) return NotFound("There are no statistics on this article");

                articleRepository.Delete(statistic);

                if (await articleRepository.CommitAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to delete statistic");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return BadRequest();
        }
    }
}
