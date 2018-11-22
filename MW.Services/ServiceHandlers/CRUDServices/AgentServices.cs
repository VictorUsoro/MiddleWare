using MW.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MW.Services
{
   public class AgentServices : BaseServices<Agent, AgentModel>
   {
      public AgentServices(MiddleWareDBContext context) : base(context)
      {
      }
   }
}
