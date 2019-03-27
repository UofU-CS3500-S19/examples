using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest;
using System.Dynamic;
using static System.Net.HttpStatusCode;
using static System.Net.Http.HttpMethod;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class ToDoTests
    {
        private RestClient client = new RestClient("http://localhost:44444/ToDo/");

        [TestMethod]
        public void TestMethod1()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(36, r.Data.Length);

            user.Name = "";
            r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod2()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(OK, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);

            item.UserID = null;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(Forbidden, r.Status);

            item.UserID = "missing";
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod3()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(OK, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);
            string itemID = r.Data.ToString();

            r = client.DoMethodAsync("PUT", "MarkCompleted/" + itemID, item).Result;
            Assert.AreEqual(NoContent, r.Status);

            r = client.DoMethodAsync("PUT", "MarkCompleted/" + "missing", item).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod4()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(OK, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);
            string itemID = r.Data.ToString();

            r = client.DoMethodAsync("DELETE", "DeleteItem/" + itemID).Result;
            Assert.AreEqual(NoContent, r.Status);

            r = client.DoMethodAsync("DELETE", "DeleteItem/" + itemID).Result;
            Assert.AreEqual(Forbidden, r.Status);

            r = client.DoMethodAsync("DELETE", "DeleteItem/" + "missing").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod5()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoMethodAsync("POST", "RegisterUser", user).Result;
            Assert.AreEqual(OK, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);
            string itemID1 = r.Data.ToString();

            item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 2";
            item.Completed = false;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);
            string itemID2 = r.Data.ToString();

            item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 3";
            item.Completed = false;
            r = client.DoMethodAsync("POST", "AddItem", item).Result;
            Assert.AreEqual(OK, r.Status);
            string itemID3 = r.Data.ToString();

            r = client.DoMethodAsync("PUT", "MarkCompleted/" + itemID1, item).Result;
            Assert.AreEqual(NoContent, r.Status);

            r = client.DoMethodAsync("DELETE", "DeleteItem/" + itemID3).Result;
            Assert.AreEqual(NoContent, r.Status);

            r = client.DoMethodAsync("GET", "GetAllItems/false/missing").Result;
            Assert.AreEqual(Forbidden, r.Status);

            r = client.DoMethodAsync("GET", "GetAllItems/false/" + userID).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(2, r.Data.Count);

            r = client.DoMethodAsync("GET", "GetAllItems/true/" + userID).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(1, r.Data.Count);
        }
    }
}

