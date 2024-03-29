﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Enums;

namespace WebApp.Controllers
{
    [ApiController]
    public class ObjectsController : ControllerBase
    {
        [HttpGet($"{{type:enum({nameof(ObjectTypeEnum)})}}")]
        public ActionResult GetByType([FromRoute] ObjectTypeEnum type)
        {
            return Ok($"{nameof(GetByType)}: {type}");
        }

        [HttpGet("four")]
        public ActionResult GetByFour()
        {
            return Ok($"{nameof(GetByFour)}: four");
        }
    }
}
