using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HRIS_WAMS_WebCoreAPI.Models
{
    public class MainSwaggerDoc: IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            OpenApiServer oaps = new OpenApiServer();
            List<OpenApiServer> loaps = new List<OpenApiServer>()
            {
                oaps
            };
            oaps.Url = @"http://10.172.18.146:8081/";

            
            OpenApiPathItem oapi = new OpenApiPathItem()
            {
                Servers = loaps
            };


            OpenApiPaths oap = new OpenApiPaths()
            {
                {"url",oapi }
            };
            swaggerDoc.Paths = oap;

            //swaggerDoc.Servers 
            //swaggerDoc.Host = "some-url-that-is-hosted-on-azure.azurewebsites.net";
            //swaggerDoc.BasePath = "/api";
            //swaggerDoc.Schemes = new List<string> { "https" };
        }


    }
}
