using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using assignmentTracker.Controllers;
using Moq;
using assignmentTracker.Models;
using System.Linq;
using System.Web.Mvc;

namespace assignmentTracker.Tests.Controllers
{
    [TestClass]
    public class AssignmentControllerTest
    {
        assignmentsController controller;
        Mock<IMockAssignmentRepository> mock;
        List<assignment> assignments;


        //This method runs automatically before each unit test
        [TestInitialize]
        public void Initialize()
        {
            //Instantiate the mock
            mock = new Mock<IMockAssignmentRepository>();

            //Instantiate mock artist data
            assignments = new List<assignment>
            {
                new assignment {assignment_id=1,title="Artists 1"},
                new assignment {assignment_id=2,title="Artists 2"}

            };
            //Bind data to mock
            mock.Setup(m => m.assignments).Returns(assignments.AsQueryable());

            //Initialize the constructor, inject the dependancy
            controller = new assignmentsController(mock.Object);

        }


        [TestMethod]
        public void IndexViewLoads()
        {
            //Act
            var actual = controller.Index();

            //Assert
            Assert.IsNotNull(actual);
        }

        //To check the index loads objects of type assignments
        [TestMethod]
        public void IndexLoadsAssignments()
        {
            //Act
            var actual = (List<assignment>)((ViewResult)controller.Index()).Model;

            //Assert
            CollectionAssert.AreEqual(assignments, actual);
        }

        // details 3 cases

        [TestMethod]
        public void DetailsWithValidId()
        {
            //act
            var actual = (assignment)((ViewResult)controller.Details(1)).Model;

            Assert.AreEqual(assignments[0], actual);
        }

        [TestMethod]
        public void DetailsWithInValidId()
        {
            //act
            var actual = (ViewResult)controller.Details(8);

            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsWithNoId()
        {
            //act
            var actual = (ViewResult)controller.Details(null);

            Assert.AreEqual("Error", actual.ViewName);
        }


        //create get
        [TestMethod]
        public void CreateViewLoads()
        {
            //act
            var actual = (ViewResult)controller.Create();

            Assert.AreEqual("Create", actual.ViewName);
        }

        //create post
        [TestMethod]
        public void CreateValid()
        {
            //arrange
            assignment test = new assignment { title = "Java assignment" };
            //act
            var actual = (RedirectToRouteResult)controller.Create(test);
            //assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInValid()
        {
            //arrange
            assignment test = new assignment { title = "Java assignment" };

            controller.ModelState.AddModelError("Id", "Unit Test");
            //act
            var actual = (ViewResult)controller.Create(test);
            //assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        //edit get
        [TestMethod]
        public void EditValidID()
        {
            //act
            var actual = ((ViewResult)controller.Edit(1)).Model;

            //assert
            Assert.AreEqual(assignments[0], actual);
        }

        public void EditInValidID()
        {
            //act
            var actual = (ViewResult)controller.Edit(9);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        public void EditNoValidID()
        {
            //arrange
            int? id = null;

            //act
            var actual = (ViewResult)controller.Edit(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        //edit post

        [TestMethod]
        public void EditPostValid()
        {
            //act 
            var actual = (RedirectToRouteResult)controller.Edit(assignments[0]);

            //assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostInValid()
        {
            //arrange
            controller.ModelState.AddModelError("Id", "Unit Test");

            //act
            var actual = (ViewResult)controller.Edit(assignments[0]);

            //assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

    }
}
