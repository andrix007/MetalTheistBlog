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
    public class BandMembersController : ControllerBase
    {
        private readonly IBandMemberRepository bandMemberRepository;
        private readonly IBandRepository bandRepository;
        private readonly LinkGenerator linkGenerator;

        public BandMembersController(IBandMemberRepository bandMemberRepository, IBandRepository bandRepository, LinkGenerator linkGenerator)
        {
            this.bandMemberRepository = bandMemberRepository;
            this.bandRepository = bandRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet] //Gets all the BandMembers from the database
        public async Task<ActionResult<List<BandMember>>> Get(bool includeBandMemberRoles = false)
        {
            try
            {
                var results = await bandMemberRepository.GetAllBandMembersAsync(includeBandMemberRoles);

                return results.OrderBy(r => r.Id).ToList();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        } 

        [HttpGet("{id:int}")] //Gets the BandMember with the id "id" from the database
        public async Task<ActionResult<BandMember>> GetBandMemberById(int id, bool includeBandMemberRoles = false)
        {
            try
            {
                var bandMember = await bandMemberRepository.GetBandMemberById(id, includeBandMemberRoles);

                if (bandMember == null) return NotFound($"There is no BandMember with id: {id}");

                return bandMember;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        } 

        [HttpGet("{id:int}/band")] //Gets a BandMember's band from the database
        public async Task<ActionResult<Band>> GetBandMemberBand(int id, bool includeAlbums = false, bool includeBandMembers = false)
        {
            try
            {
                var bandMember = await bandMemberRepository.GetBandMemberById(id);
                if (bandMember == null) return NotFound($"There is no BandMember with id: {id}");

                var band = await bandRepository.GetBandByIdAsync(bandMember.Band.Id, includeAlbums, includeBandMembers);
                if (band == null) return BadRequest($"This bandmember isn't in any bands");

                return band;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPut("{id:int}/band/{id1:int}")] //Adds a band from the Bands DbSet to a BandMember
        public async Task<ActionResult<Band>> AddExistingBandToBandMember(int id,int id1,bool includeAlbums=false,bool includeBandMembers=false)
        {
            try
            {
                var bandMember = await bandMemberRepository.GetBandMemberById(id);
                if (bandMember == null) return NotFound($"There is no BandMember with id: {id}");

                var band = await bandRepository.GetBandByIdAsync(id1, includeAlbums, includeBandMembers);
                if (band == null) return NotFound($"There is no Band with id: {id}");

                bandMember.Band = band;
                
                if (await bandRepository.CommitAsync())
                {
                    return Created("api/bandmembers/{id:int}/band", band);
                }
                else
                {
                    return BadRequest("Failed to create new Band");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        //[HttpDelete("{id:int}/band")] //BandMember leaves the band he's currently in

        [HttpPost] //Adds a BandMember to the BandMembers' table
        public async Task<ActionResult<BandMember>> Post(BandMember model, bool includeBandMemberRoles = false)
        {
            try
            {
                var existing = await bandMemberRepository.GetBandMemberById(model.Id, includeBandMemberRoles);
                if (existing != null) return BadRequest($"There is already a BandMember with id: {model.Id}");

                var url = linkGenerator.GetPathByAction("Get", "BandMembers", new { id = model.Id });
                if (string.IsNullOrWhiteSpace(url))
                {
                    return BadRequest("Could not use current id");
                }

                bandMemberRepository.Add(model);
                if (await bandMemberRepository.CommitAsync())
                {
                    return Created(url, model);
                }
                else
                {
                    return BadRequest("Failed to create new Band");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPut("{id:int}")] //Updates a BandMember
        public async Task<ActionResult<BandMember>> Put(int id, BandMember model, bool includeBandMemberRoles = false)
        {
            try
            {
                var bandMember = await bandMemberRepository.GetBandMemberById(id);
                if (bandMember == null) return NotFound($"Could not find BandMember with id of {id}");

                bandMember.Map(model);

                if (await bandMemberRepository.CommitAsync())
                {
                    return bandMember;
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to update BandMember");
        }

        [HttpDelete("{id:int}")] //Deletes a BandMember
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await bandMemberRepository.GetBandMemberById(id);
                if (existing == null) return NotFound($"Could not find BandMember with id of {id}");

                bandMemberRepository.Delete(existing);

                if (await bandMemberRepository.CommitAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to delete BandMember");
        }

    }
}
