using HRMS.Data.Entities;
using HRMS.Models;
using HRMS.Repository;
using AutoMapper;

namespace HRMS.BusinessLogic;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeModel?> GetEmployeeByIdAsync(int id);
    Task AddEmployeeAsync(EmployeeModel Employee);
    Task UpdateEmployeeAsync(EmployeeModel updatedEmployee);
    Task DeleteEmployeeAsync(EmployeeModel employee);
}

public class EmployeeService : IEmployeeService
{
    private readonly IGenericRepository<Employee> _repository;
    private readonly IMapper _mapper;

    public EmployeeService(IGenericRepository<Employee> genericRepository, IMapper mapper)
    {
        _repository = genericRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();

        var employeeList = new List<EmployeeModel>();
        foreach (var employee in employees)
        {
            employeeList.Add(_mapper.Map<EmployeeModel>(employee));
        }
        return employeeList;
    }

    public async Task<EmployeeModel?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        return _mapper.Map<EmployeeModel>(employee);
    }

    public async Task AddEmployeeAsync(EmployeeModel employeeModel)
    {
        var employee = _mapper.Map<Employee>(employeeModel);
        await _repository.InsertAsync(employee);
    }

    public async Task UpdateEmployeeAsync(EmployeeModel employeeModel)
    {
        var updatedEmployee = _mapper.Map<Employee>(employeeModel);
        await _repository.UpdateAsync(updatedEmployee);
    }

    public async Task DeleteEmployeeAsync(EmployeeModel employeeModel)
    {
        var deletedEmployee = _mapper.Map<Employee>(employeeModel);
        await _repository.DeleteAsync(deletedEmployee);
    }
}