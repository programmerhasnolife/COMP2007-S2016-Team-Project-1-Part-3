﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//required for Identity and OWIN Security
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

/**
 * @author: Yandong Wang 200277628, Zhen Zhang 200257444
 * @date: June 24, 2016
 * @version: 0.0.3 - Allow user to logout
 */

namespace COMP2007_S2016_Team_Project_1_Part_3
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //store session info and authentication methods in the authenticationManager object
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            //sign out
            authenticationManager.SignOut();

            //Redirect to the Default page
            Response.Redirect("~/Login.aspx");
        }
    }
}