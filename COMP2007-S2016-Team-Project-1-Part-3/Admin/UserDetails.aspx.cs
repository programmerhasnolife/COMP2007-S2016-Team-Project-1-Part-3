﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// required for EF DB Access
using COMP2007_S2016_Team_Project_1_Part_3.Models;
using System.Web.ModelBinding;

// required for Identity and OWIN Security
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace COMP2007_S2016_Team_Project_1_Part_3.Admin
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    PasswordPlaceHolder.Visible = false;
                    this.GetUser();
                }
                else
                {
                    PasswordPlaceHolder.Visible = true;
                }
            }
        }

        protected void GetUser()
        {
            string UserID = Request.QueryString["Id"].ToString();

            using (UserConnection db = new UserConnection())
            {
                AspNetUser updatedUser = (from user in db.AspNetUsers
                                          where user.Id == UserID
                                          select user).FirstOrDefault();

                if (updatedUser != null)
                {
                    UserNameTextBox.Text = updatedUser.UserName;
                    
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // redirect to Users page
            Response.Redirect("~/Admin/Users.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string UserID = "";

            // if updating user
            if (Request.QueryString.Count > 0)
            {
                using (UserConnection db = new UserConnection())
                {
                    AspNetUser newUser = new AspNetUser();

                    UserID = Request.QueryString["Id"].ToString();

                    newUser = (from users in db.AspNetUsers
                               where users.Id == UserID
                               select users).FirstOrDefault();

                    newUser.UserName = UserNameTextBox.Text;
                    

                    db.SaveChanges();

                    // redirect to the users list
                    Response.Redirect("~/Admin/Users.aspx");

                }
            }

            // if creating a new user
            if (UserID == "")
            {
                // create new userStore and userManager objects
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);

                // create a new user object
                var user = new IdentityUser()
                {
                    UserName = UserNameTextBox.Text,
                   
                };

                // create a new user in the db and store the result 
                IdentityResult result = userManager.Create(user, PasswordTextBox.Text);

                if (result.Succeeded)
                {
                    Response.Redirect("~/Admin/Users.aspx");
                }
                else
                {
                    StatusLabel.Text = result.Errors.FirstOrDefault();
                    AlertFlash.Visible = true;
                }
            }
        }
    }
}