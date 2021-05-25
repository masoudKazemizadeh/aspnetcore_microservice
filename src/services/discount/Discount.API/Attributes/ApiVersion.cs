using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Discount.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =true)]
    public class ApiVersion : Attribute, IActionConstraint
    {
        private string version;
        public int Order => 0;
        private const string HeaderKey = "Content-Type";
        public ApiVersion(string apiVersion)
        {
            version = apiVersion;
        }
        public bool Accept(ActionConstraintContext context)
        {
            if (string.IsNullOrEmpty(version))
                return true;

            var requestHeader = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeader.ContainsKey("content-type"))
                return false;
            var contentType = requestHeader.First(x => x.Key.Equals(HeaderKey,StringComparison.InvariantCultureIgnoreCase)).Value;
            return Regex.IsMatch(contentType, $@"\b{version}\b");


        }
    }
}
