using CC1AspMobileApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CC1AspMobileApi.Controllers
{
    public class ProprietaireController : ApiController
    {
        private cc_dbEntities db = new cc_dbEntities();
        [HttpGet]
       
        public IHttpActionResult ListeProprietaire(int index = 0, int taille = 10)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                var proprietaire = db.Proprietaires.Include(nameof(Proprietaire)).OrderByDescending(x => x.Bien1).
                    Skip(index * taille + 1).Take(taille).ToList();
                return Ok(proprietaire);
            }
            catch (DbUpdateException ex)
            {
                var exception = ex.InnerException?.InnerException as SqlException;
                return BadRequest(exception?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProprietaire(int id)
        {
            try
            {
                var proprietaire = db.Proprietaires.Find(id);
                return Ok(proprietaire);
            }
            catch (DbUpdateException ex)
            {
                var exception = ex.InnerException?.InnerException as SqlException;
                return BadRequest(exception?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
