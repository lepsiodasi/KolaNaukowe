using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KolaNaukowe.web.Dtos;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Repositories;

namespace KolaNaukowe.web.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _genericRepository;
        private readonly IMapper _mapper;

        public StudentService(IGenericRepository<Student> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public StudentDto Add(Student newStudent)
        {
            var student = _genericRepository.Add(newStudent);
            return _mapper.Map<Student, StudentDto>(student);
        }

        public StudentDto Get(int id)
        {
            var student = _genericRepository.Get(id);
            return _mapper.Map<Student, StudentDto>(student);
        }

        public IEnumerable<StudentDto> GetAll()
        {
            var students = _genericRepository.GetAll().OrderBy(c => c.Name);
            return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);
        }

        public void Remove(int id)
        {
            _genericRepository.Remove(id);
        }

        public void Update(Student item)
        {
            _genericRepository.Update(item);
        }
    }

}
