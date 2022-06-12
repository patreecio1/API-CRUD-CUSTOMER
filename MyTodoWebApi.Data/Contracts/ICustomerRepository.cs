
using MyTodoWebApi.ApiModel;
using MyTodoWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTodoWebApi.Data.Contracts
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<bool> AddCustomer(CustomerVM customer);

        Task<Customer> GetCustomerbyID(int id);

        Task<bool> UpdateCustomer(CustomerVM customer);

        Task<bool> DeleteCustomer(int Id);

    }
}
