using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using  Blogpost.Core.Application.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;




namespace Blogpost.API.Area.Application
{
    public class ApiControllerBase : Controller
    {
        internal string MethodType { get; set; }
        internal string Token { get; set; }
        internal ResponseModel ResponseModel { get; private set; }
        protected PostResponseModel PostResponseModel { get { return ResponseModel as PostResponseModel; } }
        

        public Core.User.Model.UserSessionModel UserSession { get; set; }



        public ApiControllerBase()
        {
            ResponseModel = new ResponseModel();
        }

        internal void SetPostResponseModel(string type)
        {
            ResponseModel = new PostResponseModel
            {
                ModelValidation = new ModelValidationOutput { IsValid = true }
            };
            MethodType = type;
        }

        internal void SetResponseModel(string type)
        {
            ResponseModel = new ResponseModel();
            MethodType = type;
        }

        internal ResponseModel SetData(object data)
        {
            ResponseModel.Data = data;
            return ResponseModel;
        }
        internal void SetActionArguments(IEnumerable<KeyValuePair<string, object>> actionArguments)
        {
            ResponseModel.Data = actionArguments.Count() == 1 ? actionArguments.First().Value : actionArguments.Select(m => Tuple.Create(m.Key, m.Value)).ToArray();
        }

        internal void SetValidationResult(ModelStateDictionary state)
        {
            ResponseModel = PostResponseModel == null ? new PostResponseModel() : ResponseModel;
            PostResponseModel.ModelValidation = new ModelValidationOutput
            {
                IsValid = state.IsValid,
                Errors = state.Where(x => x.Value != null && x.Value.Errors.Any())
                        .Select(x =>
                            new ModelValidationItem(x.Key.IndexOf(".") >= 0 ? x.Key.Substring(x.Key.LastIndexOf(".") + 1) : x.Key,
                            x.Value.Errors.First().ErrorMessage))
                        .ToList()
            };
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                SetValidationResult(context.ModelState);
                PostResponseModel.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                var err = PostResponseModel.ModelValidation.Errors.FirstOrDefault();
                if (err != null && !string.IsNullOrEmpty(err.Error))
                {
                    PostResponseModel.SetErrorMessage(err.Error);
                }
                else
                {
                    PostResponseModel.SetErrorMessage("Validation failed.");
                }
                var result = new ObjectResult(PostResponseModel)
                {
                    StatusCode = PostResponseModel.StatusCode
                };
                context.Result = result;
            }
            base.OnActionExecuting(context);
        }


        internal string BuildJwtToken(System.Security.Claims.Claim[] claims, Core.Application.ISettings _settings)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_settings.UserSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _settings.BaseUrl,
                audience: _settings.BaseUrl,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                notBefore: DateTime.UtcNow,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
