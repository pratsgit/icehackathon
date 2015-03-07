using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EntityModels;
using EntityExtractor;

namespace EntityServiceWorkerRole
{
    public class EntitiesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<FashionEntity> entities = EntityManager.GetEntities(Request.GetQueryNameValuePairs());
                return Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}