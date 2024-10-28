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
            IReadOnlyList<Department> departments = await _unitOfWork.DepartmentRepository.GetDepartments();
            return await ConvertToDepartmentDtos(departments);
        }

        public async Task<IReadOnlyList<DepartmentDto>> GetHomePageDepartments()
        {
            IReadOnlyList<Department> departments = await _unitOfWork.DepartmentRepository.GetHomePageDepartments();
            return await ConvertToDepartmentDtos(departments);
        }

        private async Task<IReadOnlyList<DepartmentDto>> ConvertToDepartmentDtos(IReadOnlyList<Department> departments)
        {
            IReadOnlyList<DepartmentDto> departmentDtos = _mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
            if (departmentDtos.Count > 0)
            {
                IReadOnlyList<string> departmentIds = departmentDtos.Select(c => c.Id).ToList();
                IReadOnlyList<string> mediaFileTypes = new List<string>{"Image"};
                IReadOnlyList<MediaFile> mediaFiles = await _unitOfWork.MediaFileRepository.GetMediaFilesByEntityTypeEntityIdsType("Departments", mediaFileTypes, departmentIds);
                if (mediaFiles.Count > 0)
                {
                    foreach (DepartmentDto dept in departmentDtos)
                    {
                        MediaFile? mediaFile = mediaFiles.FirstOrDefault(c => c.EntityId == dept.Id);
                        if (mediaFile != null) dept.ImageUrl = mediaFile.Url;
                    }
                }
            }
            return departmentDtos;
        }
    }
}