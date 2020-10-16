using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Data.Service
{
    public class MailTemplateRepository : CoreRepository<MailTemplate, CoreContext>
    {
        private CoreContext _coreContext;
        public MailTemplateRepository(CoreContext context) : base(context)
        {
            _coreContext = context;
        }

        public async Task<MailTemplate> GetByCode (string Code)
        {
            return await _coreContext.MailTemplate.FirstOrDefaultAsync(c => c.TemplateCode == Code);
        }
    }
}
