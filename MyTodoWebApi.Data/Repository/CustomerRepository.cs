using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using MyTodoWebApi.ApiModel;
using MyTodoWebApi.Data.Contracts;
using MyTodoWebApi.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace MyTodoWebApi.Data.Repository
{
    class CustomerRepository : EFRepository<Customer>, ICustomerRepository
    {
        private readonly customerinfoContext Db;
        public CustomerRepository(customerinfoContext context) : base(context)
        {
            Db = context;

        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await Db.Customer.Take(20).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<bool> AddCustomer(CustomerVM customer)
        {

            Customer CustomertoAdd = new Customer
            {

                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PlaceOfBirth = customer.PlaceOfBirth,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth,
               
            };

            await Db.Customer.AddAsync(CustomertoAdd);

            int result = await Db.SaveChangesAsync();

            if (result > 0) { return true; } else { return false; }
        }

        public async Task<Customer> GetCustomerbyID(int id)
        {

            return await Db.Customer.Where(x => x.Id == id).SingleOrDefaultAsync();

        }

        public async Task<bool> UpdateCustomer(CustomerVM customer)
        {

            Customer CustomertoUpdate = await Db.Customer.Where(x => x.Id == customer.Id).SingleOrDefaultAsync();
            if (CustomertoUpdate == null) { return false; }
            else
            {

                CustomertoUpdate.FirstName = customer.FirstName;
                CustomertoUpdate.LastName = customer.LastName;  
                CustomertoUpdate.Address = customer.Address;
                CustomertoUpdate.DateOfBirth = customer.DateOfBirth;
                CustomertoUpdate.PlaceOfBirth = customer.PlaceOfBirth;
                int result = await Db.SaveChangesAsync();

                if (result > 0) { return true; } else { return false; }

            }

        }

        public async Task<bool> DeleteCustomer(int Id)
        {
            Customer customer = await Db.Customer.Where(x => x.Id == Id).SingleOrDefaultAsync();
            if (customer == null) { return false; }

            try
            {

                Db.Customer.Remove(customer);
                await Db.SaveChangesAsync();
                return true;


            }
            catch (Exception)
            {
                return false;
            }

        }

    }
    }




