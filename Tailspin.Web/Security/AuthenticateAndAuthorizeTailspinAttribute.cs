﻿//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Tailspin.Web.Security
{
    using System;
    using System.Web.Mvc;
    using Microsoft.IdentityModel.Protocols.WSFederation;
    using Microsoft.IdentityModel.Web;
    using Samples.Web.ClaimsUtillities;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthenticateAndAuthorizeTailspinAttribute : AuthenticateAndAuthorizeRoleAttribute
    {
        protected override WSFederationMessage BuildSignInMessage(AuthorizationContext context, Uri replyUrl)
        {
            var fam = FederatedAuthentication.WSFederationAuthenticationModule;
            var signIn = new SignInRequestMessage(new Uri(fam.Issuer), fam.Realm)
            {
                Context = AuthenticateAndAuthorizeRoleAttribute.GetReturnUrl(context.RequestContext, RequestAppendAttribute.RawUrl, null).ToString(),
                HomeRealm = Tailspin.Federation.HomeRealm,
                Reply = replyUrl.ToString()
            };

            return signIn;
        }
    }
}
