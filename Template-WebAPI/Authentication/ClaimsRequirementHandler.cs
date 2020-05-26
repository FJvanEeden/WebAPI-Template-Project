﻿using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirment policyRequirement)
    {
      var tokenClaimsToList = context.User.Claims.Select(c => c.Value).ToList();

      var claimsValue = tokenClaimsToList[4].Substring(0, tokenClaimsToList[4].Length);

      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
      };

      var claimsJsonModel = JsonSerializer.Deserialize<ClaimsFromToken>(claimsValue, options);

      List<string> listOfTokenClaims = new List<string>();

      foreach(var item in claimsJsonModel.Groups.ElementAt(0).Roles)
      {
        var roleToString = item.RoleName.ToString();
        listOfTokenClaims.Add(roleToString);
      }

      string policyRequirment = policyRequirement.MatchingRole;

      if (listOfTokenClaims.Contains(policyRequirment))
      {
        context.Succeed(policyRequirement);
      }
      return Task.CompletedTask;
    }
  }

  

}
