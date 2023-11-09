//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AlmacenApi.Helpers
//{
//    public class FiltroErrores: Exceptionfilterattribute
//    {
//        private readonly ILogger<FiltroErrores> logger;

//        public FiltroErrores(Ilogger<FiltroErrores> logger)
//        {
//            this.logger = logger;
//        }

//        public override void OnException (ExceptionContext contex)
//        {
//            logger.LogError(context.Exception, context.Exception.Message);
//            base.OnException(context);
//        }

//    }
//}
