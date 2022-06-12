using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTodoWebApi.Data;
using MyTodoWebApi.ApiModel;
using MyTodoWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace MyTodoAppWebApi.Controllers
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IUnitOfWork UnitOfWork;
        private ILogger<CustomerController> logger;

        public CustomerController(IUnitOfWork _unitOfWork, ILogger<CustomerController> _logger) { this.UnitOfWork = _unitOfWork; logger = _logger; }
        //1

        [HttpGet]
        [Route("LoadCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {

            try
            {

                List<Customer> customer = await UnitOfWork.Customer.GetAllCustomers();

                if (customer.Count() < 1) { return NotFound(new { message = "No Customer Found" }); }
                else
                {

                    return Ok(new { Count = customer.Count(), Data = customer }); ;
                }
            }
            catch (Exception ex)
            {
                //log Exception
               // Logger logger = LogManager.GetLogger("databaseLogger");

                // add custom message and pass in the exception
               // logger.Error(ex, "Whoops!");
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult<Customer>> Post([FromBody] CustomerVM customer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool Result = await UnitOfWork.Customer.AddCustomer(customer);

                    if (Result == false) { return BadRequest(new { Message = "Could Not Add Customer" }); }
                    else
                    {

                        return Ok(new { Message = "Customer Successfully Added " });
                    }


                }
                else { return BadRequest(new { Message = "Model Error" }); }

            }
            catch (Exception ex)
            {
                //log Exception
                //Logger logger = LogManager.GetLogger("databaseLogger");

                //// add custom message and pass in the exception
                //logger.Error(ex, "Whoops!");
                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<ActionResult<Customer>> Put([FromBody] CustomerVM customer)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var Customer = await UnitOfWork.Customer.GetCustomerbyID(customer.Id);
                    if (customer == null) { return BadRequest(new { Message = "Customer Dose Not Exist" }); }

                    bool Result = await UnitOfWork.Customer.UpdateCustomer(customer);

                    if (Result == false) { return BadRequest(new { Message = "Could Not Add Customer" }); }
                    else
                    {

                        return Ok(new { Message = "Customer Successfully Added " });
                    }


                }
                else { return BadRequest(new { Message = "Model Error" }); }

            }
            catch (Exception ex)
            {
                //log Exception
                //Logger logger = LogManager.GetLogger("databaseLogger");

                //// add custom message and pass in the exception
                //logger.Error(ex, "Whoops!");
                return StatusCode(500);
            }

        }

        [HttpDelete]
        [Route("DeleteCustomer/{Id}")]
        public async Task<ActionResult<string>> DeleteCustomer(int Id)
        {

            try
            {

                bool Result = await UnitOfWork.Customer.DeleteCustomer(Id);

                if (Result == false) { return NotFound(new { message = "Could Not Delete Customer" }); }
                else
                {

                    return Ok(new { Count = 1, Message = "Customer Successfully Deleted" }); ;
                }
            }
            catch (Exception ex)
            {
                //log Exception
                //Logger logger = LogManager.GetLogger("databaseLogger");

                //// add custom message and pass in the exception
                //logger.Error(ex, "Whoops!");
                return StatusCode(500);
            }

        }

    }
}

