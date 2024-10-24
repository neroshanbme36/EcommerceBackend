using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DepartmentDto>> GetDepartments()
        {
            IReadOnlyList<Department> departments = await _unitOfWork.DepartmentRepository.GetAll();
            return _mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
        }
    }
}