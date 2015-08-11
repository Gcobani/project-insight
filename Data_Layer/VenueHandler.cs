using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight.Data
{
    public class VenueHandler
    {
        public bool NewVenue(Venue _venue)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@VenueCode", _venue.VenueCode),
                new SqlParameter("@VenueName", _venue.VenueName),
                new SqlParameter("@VenueSize",_venue.VenueSize),
            };
            return DataAccess.ExecuteNonQuery("sp_InsertVenue", CommandType.StoredProcedure,
                Params);
        }

        public Venue GetVenue(string VenueCode)
        {
            Venue _venue = null;

            SqlParameter[] Params = { new SqlParameter("@VenueCode", VenueCode) };
            using (DataTable table = DataAccess.ExecuteParamatizedSelectCommand("sp_GetVenue",
                CommandType.StoredProcedure, Params))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    _venue = new Venue();
                    _venue.VenueSize = Convert.ToInt32(row["VenueSize"]);
                    _venue.VenueCode = row["VenueCode"].ToString();
                    _venue.VenueName = row["VenueName"].ToString();
                }
            }
            return _venue;
        }

        public List<Venue> GetAllVenues()
        {
            List<Venue> _venueList = null;

            using (DataTable table = DataAccess.ExecuteSelectCommand("sp_GetAllVenues",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    _venueList = new List<Venue>();
                    foreach (DataRow row in table.Rows)
                    {
                        Venue _module = new Venue();
                        _module.VenueSize = Convert.ToInt32(row["VenueSize"]);
                        _module.VenueCode = row["VenueCode"].ToString();
                        _module.VenueName = row["VenueName"].ToString();
                        _venueList.Add(_module);
                    }
                }
            }
            return _venueList;
        }
    }
}
