using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Farmacity.Api.Models;

namespace Farmacity.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected string GetModelErrors()
        {
            var errors = new StringBuilder();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Append(error.ErrorMessage + ", ");
                }
            }
            return errors.ToString().Substring(0, errors.Length - 2);
        }

        protected ResponseMessageResult CreateHttpErrorMessage(string message)
        {
            var response = Request.CreateResponse(HttpStatusCode.PreconditionFailed,
                new GenericResponse { ErrorMessage = message });

            return ResponseMessage(response);
        }

        protected ResponseMessageResult ReturnErrorMessage(string message)
        {
            var response = Request.CreateResponse(HttpStatusCode.PreconditionFailed,
                new GenericResponse { ErrorMessage = message });

            return ResponseMessage(response);
        }

        protected static string GetConnectionFromConfiguration()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
