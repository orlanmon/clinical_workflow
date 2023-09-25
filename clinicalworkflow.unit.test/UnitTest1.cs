using clincalworkflow.web.app.Controllers;
using clincalworkflow.web.app.Services;
using clinicalworkflow.web.services.dto;
using Moq;
using System;
using Xunit;

namespace clinicalworkflow.unit.test
{
    public class UnitTest1
    {

        public Mock<IUnitTestService> mock = new Mock<IUnitTestService>();

        [Fact]
        public void Test1()
        {

            PatientDTO patientDTO = new PatientDTO { FirstName = "Orlando", LastName = "Monaco", City = "East Aurora", AddressOne = "Address Edit5", State = "NY", PatientId = 1, Zip = "14052"
            };


            // Confirms the Service Behavior
            //mock.Setup(uts => uts.GetPatient(1)).ReturnsAsync(patientDTO);

            // Pass the Mock Service to the Controller
            UnitTestController unitTestController = new UnitTestController(mock.Object);

            // Confirm the Controller Behavior with the Mock Service
            var result = unitTestController.GetPatientByID(1);

            Assert.True(patientDTO.Equals(result.Result));


        }
    }
}
