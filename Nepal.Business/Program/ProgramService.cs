using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class ProgramService : IProgramService
    {
        private ProgramRepository _programRepository;
        private readonly IMapper _mapper;
        public ProgramService(ProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }
        public async Task AddProgram(ProgramModel model)
        {
            var program = _mapper.Map<Program>(model);
            program.Status = 1;
            program.CreatedBy = "System";
            program.CreatedOn = DateTime.Now;
            await _programRepository.Add(program);
        }

        public async Task DeleteProgram(int Id)
        {
            await _programRepository.Delete(Id);
        }

        public async Task<ProgramViewModel> GetProgram(int Id)
        {
            var order = await _programRepository.Get(Id);
            return _mapper.Map<ProgramViewModel>(order);
        }

        public async Task<List<ProgramViewModel>> GetPrograms(string userId)
        {
            var orders = await _programRepository.GetPrograms(userId);
            return _mapper.Map<List<ProgramViewModel>>(orders);
        }

        public async Task<List<ProgramViewModel>> GetPrograms(int OrderId)
        {
            var orders = await _programRepository.GetPrograms(OrderId);
            return _mapper.Map<List<ProgramViewModel>>(orders);
        }

        public async Task UpdateProgram(ProgramModel model, int Id)
        {
            var program = await _programRepository.Get(Id);
            program.OrderId = model.OrderId;
            program.Destination = model.Destination;
            program.Quantity = model.Quantity;
            program.TruckNo = model.TruckNo;
            program.Status = model.Status;
            program.ModifiedBy = "System";
            program.ModifiedOn = DateTime.Now;

            await _programRepository.Update(program);

        }
    }
}
