using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IProgramService
    {
        public Task AddProgram(ProgramModel model);
        public Task<ProgramViewModel> GetProgram(int Id);
        public Task<List<ProgramViewModel>> GetPrograms(string userId);
        public Task<List<ProgramViewModel>> GetPrograms(int OrderId);
        public Task DeleteProgram(int Id);
        public Task UpdateProgram(ProgramModel model, int Id);
    }
}
