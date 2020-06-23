using Raunstrup.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace Raunstrup.Service.Domain
{
    public interface IContactService
    {
        bool SendEmail(Contact contact);
    }

    
    
}
