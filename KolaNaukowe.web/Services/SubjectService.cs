using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KolaNaukowe.web.Models;
using KolaNaukowe.web.Repositories;

namespace KolaNaukowe.web.Services
{
    public class SubjectService : ISubjectService
    {

        private readonly IGenericRepository<Subject> _genericRepository;
        private readonly IMapper _mapper;


        public SubjectService(IGenericRepository<Subject> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }


        public void Remove(int id)
        {
            _genericRepository.Remove(id);
        }
    }
}
