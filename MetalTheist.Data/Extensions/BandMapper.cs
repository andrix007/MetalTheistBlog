using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Extensions
{
    public static class BandMapper
    {
        public static void Map(this Band band, Band otherBand)
        {
            if (otherBand.Name != null) band.Name = otherBand.Name;
            if (otherBand.Country != null) band.Country = otherBand.Country;
            if (otherBand.City != null) band.City = otherBand.City;
            if (otherBand.Website != null) band.Website = otherBand.Website;
            if (otherBand.Discography != null) band.Discography = otherBand.Discography;
            if (otherBand.BandMembers != null) band.BandMembers = otherBand.BandMembers;
            if (otherBand.Genres != null) band.Genres = otherBand.Genres;
        }
    }
}
