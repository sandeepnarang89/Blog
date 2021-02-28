using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogpost.API.Application.Enum
{
    public static class HttpMethodType
    {
        public const string Get = "get";
        public const string Post = "post";
        public const string Delete = "delete";
        public const string Put = "put";
        public const string Patch = "patch";

        public static string FindType(string type)
        {
            var lowerType = type.ToLower();
            if (type.Equals(Get)) return Get;
            if (type.Equals(Post)) return Post;
            if (type.Equals(Put)) return Put;
            if (type.Equals(Delete)) return Delete;
            if (type.Equals(Patch)) return Patch;

            throw new Exception("Http Method not found");
        }
    }
}
