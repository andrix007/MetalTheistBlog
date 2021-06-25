using MetalTheist.Data.Entities;
using MetalTheist.Data.Extensions;
using MetalTheist.Data.Interfaces;
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
    public class BandsController : ControllerBase
    {
        private readonly IBandRepository bandRepository;
        private readonly LinkGenerator linkGenerator;

        public BandsController(IBandRepository bandRepository, LinkGenerator linkGenerator)
        {
            this.bandRepository = bandRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Band>>> Get(bool includeAlbums = false, bool includeBandMembers = false)
        {
            try
            {
                var results = await bandRepository.GetAllBandsAsync(includeAlbums,includeBandMembers);

                return results.OrderBy(r => r.Id).ToList();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Band>> GetBandById(int id, bool includeAlbums = false, bool includeBandMembers = false)
        {
            try
            {
                var band = await bandRepository.GetBandByIdAsync(id, includeAlbums, includeBandMembers);

                if (band == null) return NotFound($"There is no band with id: {id}");

                return band;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Band>> Post(Band model, bool includeAlbums = false, bool includeBandMembers = false)
        {
            try
            {
                var existing = await bandRepository.GetBandByIdAsync(model.Id, includeAlbums, includeBandMembers);
                if (existing != null) return BadRequest($"There is already a band with id: {model.Id}");

                var url = linkGenerator.GetPathByAction("Get", "Bands", new { id = model.Id });
                if(string.IsNullOrWhiteSpace(url))
                {
                    return BadRequest("Could not use current id");
                }

                bandRepository.Add(model);

                if(await bandRepository.CommitAsync())
                {
                    return Created(url, model);
                }
                else
                {
                    return BadRequest("Failed to create new Band");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await bandRepository.GetBandByIdAsync(id);
                if (existing == null) return NotFound($"Could not find Band with id of {id}");

                bandRepository.Delete(existing);

                if (await bandRepository.CommitAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to delete Band");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Band>> Put(int id, Band model, bool includeAlbums = false, bool includeBandMembers = false)
        {
            try
            {
                var band = await bandRepository.GetBandByIdAsync(id);
                if (band == null) return NotFound($"Could not find Band with id of {id}");

                band.Map(model);

                if (await bandRepository.CommitAsync())
                {
                    return band;
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to update Band");
        }
    }
}
